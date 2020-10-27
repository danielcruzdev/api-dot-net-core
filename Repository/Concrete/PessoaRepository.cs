using Dapper;
using Entity;
using Helpers;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Repository
{
    public class PessoaRepository : BaseDapper, IPessoaRepository
    {
        public PessoaRepository(SettingsApplication configuration) : base(configuration)
        {
        }

        public async Task<IEnumerable<Pessoa>> GetAllAsync()
        {
            return await QueryAsync<Pessoa>("spsPessoas");
        }
        public async Task<IEnumerable<Pessoa>> GetAllActiveAsync()
        {
            return await QueryAsync<Pessoa>("spsPessoasAtivas");
        }
        public async Task<Pessoa> GetByIdAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            var pessoa = await QueryAsync<Pessoa>("spsPessoa", parameters);
            return pessoa.FirstOrDefault();
        }
        public async Task<int> CreateAsync(string parametrosJson)
        {

            var parameters = new DynamicParameters();
            parameters.Add("@TabParametrosJSON", parametrosJson, DbType.String);
            parameters.Add("@Id", null, DbType.Int32, ParameterDirection.Output);


            await ExecuteSPAsync("spiPessoa", parameters);
            return parameters.Get<int>("@Id");
        }
        public async Task<int> UpdateAsync(string parametrosJson)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@TabParametrosJSON", parametrosJson, DbType.String);
            parameters.Add("@Id", null, DbType.Int32, ParameterDirection.Output);

            await ExecuteSPAsync("spuPessoa", parameters);
            return parameters.Get<int>("@Id");
        }
        public async Task<bool> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return false;
            }
            var parameters = new DynamicParameters();
            parameters.Add("@Id", id, DbType.Int32);

            await ExecuteSPAsync("spdPessoa", parameters);

            return true;
        }
    }

}