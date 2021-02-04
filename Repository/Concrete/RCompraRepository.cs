using api_dot_net_core.Entities.RCompras;
using api_dot_net_core.Repository.Contract;
using Dapper;
using Helpers;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace api_dot_net_core.Repository.Concrete
{
    public class RCompraRepository : BaseDapper, IRCompraRepository
    {
        private readonly string _connectionString;
        public RCompraRepository(SettingsApplication configuration) : base(configuration)
        {
            _connectionString = configuration.ConnectionString;
        }

        public async Task<RCompra> ReportData(string tabParametros)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TabParametrosJSON", tabParametros, DbType.String);

            var reportData = new RCompra();

            var clienteDictionary = new Dictionary<int, RCompraCliente>();
            var empresaDictionary = new Dictionary<(int, int), RCompraEmpresa>();
            var categoriaDictionary = new Dictionary<(int, int, int), RCompraCategoria>();
            var produtoDictionary = new Dictionary<(int, int, int, int), RCompraProduto>();

            using (var transactionScope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
            {
                using (var sqlConnection = new SqlConnection(_connectionString))
                {
                    await sqlConnection.OpenAsync();

                    using (var resultSets = await sqlConnection.QueryMultipleAsync("sprCompras", parameters, commandType: CommandType.StoredProcedure))
                    {
                        var cabecalho = new RCompraCabecalho();

                        try
                        {
                            cabecalho = resultSets.Read<RCompraCabecalho>().FirstOrDefault();

                            _ = resultSets.Read<RCompraCliente,
                            RCompraEmpresa,
                            RCompraCategoria,
                            RCompraProduto,
                            RCompraCliente>(
                            (cliente, empresa, categoria, produto) =>
                            {

                                if (!clienteDictionary.TryGetValue(cliente.ClienteId, out RCompraCliente reportCliente))
                                {
                                    reportCliente = cliente;
                                    clienteDictionary.Add(reportCliente.ClienteId, reportCliente);
                                }

                                var empresaKey = (reportCliente.ClienteId, empresa.EmpresaId);
                                if (!empresaDictionary.TryGetValue(empresaKey, out RCompraEmpresa reportEmpresa))
                                {
                                    reportEmpresa = empresa;
                                    empresaDictionary.Add(empresaKey, reportEmpresa);
                                    reportCliente.Empresas.Add(reportEmpresa);
                                }

                                var categoriaKey = (reportCliente.ClienteId, reportEmpresa.EmpresaId, categoria.CategoriaId);
                                if (!categoriaDictionary.TryGetValue(categoriaKey, out RCompraCategoria reportCategoria))
                                {
                                    reportCategoria = categoria;
                                    categoriaDictionary.Add(categoriaKey, reportCategoria);
                                    reportEmpresa.Categorias.Add(reportCategoria);
                                }

                                var produtoKey = (reportCliente.ClienteId, 
                                                  reportEmpresa.EmpresaId, 
                                                  reportCategoria.CategoriaId, 
                                                  produto.ProdutoId);

                                if (!produtoDictionary.TryGetValue(produtoKey, out RCompraProduto reportProduto))
                                {
                                    reportProduto = produto;
                                    produtoDictionary.Add(produtoKey, reportProduto);
                                    reportCategoria.Produtos.Add(reportProduto);
                                }

                                return reportCliente;

                            }, splitOn: "ClienteId,EmpresaId,CategoriaId,ProdutoId").ToList();


                            reportData.Clientes = clienteDictionary.Values.ToList();
                            reportData.Cabecalho = cabecalho;
                        }
                        catch (System.InvalidOperationException)
                        {
                            // Não fazer nada, pois sabemos que esta Exception é gerada por causa que a 
                            // Stored Procedure não retorna nenhum valor
                        }
                    }
                }

                transactionScope.Complete();
            }

            return reportData;
        }
    }
}
