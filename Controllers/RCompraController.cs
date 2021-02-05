using api_dot_net_core.Services.Contract;
using api_dot_net_core.ViewModels.RCompras;
using api_dot_net_core.ViewModels.RCompras.Demonstracao;
using api_dot_net_core.ViewModels.RCompras.Parametros;
using api_dot_net_core.ViewModels.Shared;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using TemplateApi.Api.Infrastructure.Helpers;

namespace api_dot_net_core.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RCompraController : Controller
    {
        private readonly IRComprasService _rComprasService;
        private readonly IMapper _mapper;

        public RCompraController(IMapper mapper,
            IRComprasService rComprasService)
        {
            _mapper = mapper;
            _rComprasService = rComprasService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(RCompraViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportData([FromQuery] ParametrosRelatorio parametros)
        {
            var tabParametros = parametros.ConvertModelToJson();

            var relatorio = await _rComprasService.ReportData(tabParametros);
            var viewModel = _mapper.Map<RCompraViewModel>(relatorio);

            return Ok(viewModel);
        }

        [HttpGet("Wrong")]
        [ProducesResponseType(typeof(RCompraGeralDemonstracaoViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ReportWrongData([FromQuery] ParametrosRelatorio parametros)
        {
            var tabParametros = parametros.ConvertModelToJson();

            var relatorio = await _rComprasService.ReportWrongData(tabParametros);
            var viewModel = _mapper.Map<RCompraGeralDemonstracaoViewModel>(relatorio);

            return Ok(viewModel);
        }

        [HttpGet("export")]
        [ProducesResponseType(typeof(MidiaContentViewModel), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ExportRelatorio([FromQuery] ParametrosRelatorio parametros)
        {

            string tabParametros = parametros.ConvertModelToJson();
            var reportData = await _rComprasService.ReportData(tabParametros);
            var relatorio = _mapper.Map<RCompraViewModel>(reportData);

            try
            {
                if (reportData.Clientes.Any())
                {
                    var workBook = CreateExcel(relatorio);
                    var midia = GetMidia(workBook);

                    return File(midia.Content, midia.ContentType, midia.Name);
                }
                else
                    return Ok();
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
        }

        //Create Excel Method
        private IWorkbook CreateExcel(RCompraViewModel relatorio)
        {
            const int startIndex = 0;
            int imgColumnQtt = 3;
            int totalColumnQtt = 8;

            int headerColumnQtt = totalColumnQtt - imgColumnQtt;

            IWorkbook workbook = new XSSFWorkbook();
            var styles = CreateStyles(workbook);
            var tableColumn = styles[3];
            var tableColumnGrey = styles[4];

            ISheet sheet = workbook.CreateSheet("Compras");
            int rowIndex = startIndex;
            var produtosCount = 0;

            rowIndex = CreateExcelHeader(relatorio.Cabecalho, startIndex, imgColumnQtt, totalColumnQtt, headerColumnQtt, rowIndex, sheet, styles);

            foreach (var cliente in relatorio.Clientes)
            {
                if (!relatorio.Clientes.Any())
                    CreateNoDataFoundCells(tableColumn, sheet, totalColumnQtt, rowIndex);
                else
                {
                    foreach (var empresa in cliente.Empresas)
                    {
                        foreach (var categoria in empresa.Categorias)
                        {
                            foreach (var produto in categoria.Produtos)
                            {
                                produtosCount = categoria.Produtos.Count();
                                IRow row = sheet.CreateRow(rowIndex++);
                                int cellIndex = startIndex;

                                var styleTable = rowIndex % 2 == 0 ? tableColumnGrey : tableColumn;

                                ExcelUtils.CreateCell(row, styleTable, cellIndex++, cliente.ClienteNome);
                                ExcelUtils.CreateCell(row, styleTable, cellIndex++, cliente.ClienteTelefone);
                                ExcelUtils.CreateCell(row, styleTable, cellIndex++, empresa.EmpresaNome);
                                ExcelUtils.CreateCell(row, styleTable, cellIndex++, empresa.EmpresaTelefone);
                                ExcelUtils.CreateCell(row, styleTable, cellIndex++, empresa.EmpresaCidade);
                                ExcelUtils.CreateCell(row, styleTable, cellIndex++, categoria.CategoriaNome);
                                ExcelUtils.CreateCell(row, styleTable, cellIndex++, produto.ProdutoNome);
                                ExcelUtils.CreateCell(row, styleTable, cellIndex++, produto.ValorDesc);
                            }
                        }
                    }
                }
            }
            ExcelUtils.SetExcelAuthor(workbook);

            return workbook;
        }

        //Create Excel Header
        private int CreateExcelHeader(RCompraCabecalhoViewModel cabecalho, int startIndex, int imgColumnQtt, int totalColumnQtt, int headerColumnQtt, int rowIndex, ISheet sheet, List<XSSFCellStyle> styles)
        {
            var header1 = styles[0];
            var header2 = styles[1];
            var header3 = styles[2];
            var tableColumn = styles[3];

            int mediumWidth = 16 * 256;

            IRow row1 = sheet.CreateRow(rowIndex++);
            IRow row2 = sheet.CreateRow(rowIndex++);
            IRow row3 = sheet.CreateRow(rowIndex++);
            IRow row4 = sheet.CreateRow(rowIndex++);
            IRow row5 = sheet.CreateRow(rowIndex++);
            IRow row6 = sheet.CreateRow(rowIndex++);
            IRow row7 = sheet.CreateRow(rowIndex++);

            row2.HeightInPoints = 26;
            row3.HeightInPoints = 25;
            for (var index = 0; index <= totalColumnQtt; index++)
            {
                sheet.SetColumnWidth(index, mediumWidth);
            }

            var rowGroups = new List<List<GroupColumnConfig>>
            {
				// 1a Linha
				new List<GroupColumnConfig>
                {
                    new GroupColumnConfig(row1, header1, "Estudos", headerColumnQtt, true),
                    new GroupColumnConfig(row1, header1, null, imgColumnQtt)
                },
				// 2a Linha
				new List<GroupColumnConfig>
                {
                    new GroupColumnConfig(row2, header1, ReportTextInfo(), headerColumnQtt, true),
                    new GroupColumnConfig(row2, header1, null, imgColumnQtt)
                },
			  // 3a Linha
			  new List<GroupColumnConfig>
              {
                  new GroupColumnConfig(row3, header2, LoteCellText(cabecalho), headerColumnQtt, true),
                  new GroupColumnConfig(row3, header2, null, imgColumnQtt)
              },
			  // 4a Linha
			  new List<GroupColumnConfig>
              {
                  new GroupColumnConfig(row4, header2, LoginCellText(cabecalho), headerColumnQtt, true),
                  new GroupColumnConfig(row4, header2, null, imgColumnQtt, true, true, new MergeColumnConfig(row1.RowNum, row4.RowNum, headerColumnQtt, totalColumnQtt - 1))
              },
			  // 5a Linha
			  new List<GroupColumnConfig>
              {
                  new GroupColumnConfig(row5, tableColumn, null, totalColumnQtt, true)
              },
			  // 6a Linha
			  new List<GroupColumnConfig>
              {
                  new GroupColumnConfig(row6, header3, "Nome", 1),
                  new GroupColumnConfig(row6, header3, "Telefone", 1),
                  new GroupColumnConfig(row6, header3, "Empresa", 1),
                  new GroupColumnConfig(row6, header3, "Telefone Empresa", 1),
                  new GroupColumnConfig(row6, header3, "Cidade", 1),
                  new GroupColumnConfig(row6, header3, "Categoria", 1),
                  new GroupColumnConfig(row6, header3, "Produto", 1),
                  new GroupColumnConfig(row6, header3, "Valor Agregado", 1),
              },
			  // 7a Linha
			  new List<GroupColumnConfig>
              {
                  new GroupColumnConfig(row7, header3, null, 1, true, true, new MergeColumnConfig(row6.RowNum, row7.RowNum, 0, 0)),
                  new GroupColumnConfig(row7, header3, null, 1, true, true, new MergeColumnConfig(row6.RowNum, row7.RowNum, 1, 1)),
                  new GroupColumnConfig(row7, header3, null, 1, true, true, new MergeColumnConfig(row6.RowNum, row7.RowNum, 2, 2)),
                  new GroupColumnConfig(row7, header3, null, 1, true, true, new MergeColumnConfig(row6.RowNum, row7.RowNum, 3, 3)),
                  new GroupColumnConfig(row7, header3, null, 1, true, true, new MergeColumnConfig(row6.RowNum, row7.RowNum, 4, 4)),
                  new GroupColumnConfig(row7, header3, null, 1, true, true, new MergeColumnConfig(row6.RowNum, row7.RowNum, 5, 5)),
                  new GroupColumnConfig(row7, header3, null, 1, true, true, new MergeColumnConfig(row6.RowNum, row7.RowNum, 6, 6)),
                  new GroupColumnConfig(row7, header3, null, 1, true, true, new MergeColumnConfig(row6.RowNum, row7.RowNum, 7, 7)),
              }
            };

            CreateTableColumns(sheet, rowGroups, startIndex);
            //ExcelUtils.InsertImage(sheet, totalColumnQtt - imgColumnQtt, totalColumnQtt, cabecalho);

            sheet.CreateFreezePane(row1.RowNum, row7.RowNum + 1);

            return rowIndex;
        }

        //Create Excel Styles
        private List<XSSFCellStyle> CreateStyles(IWorkbook workbook)
        {
            List<XSSFCellStyle> styles = new List<XSSFCellStyle>();

            var simpleDataFormat = workbook.CreateDataFormat().GetFormat("#,##0;-#,##0");

            var font12Bold = workbook.CreateFont();
            font12Bold.FontHeightInPoints = 12;
            font12Bold.IsBold = true;

            var font10 = workbook.CreateFont();
            font10.FontHeightInPoints = 10;
            font10.IsBold = false;

            var font10Bold = workbook.CreateFont();
            font10Bold.FontHeightInPoints = 10;
            font10Bold.IsBold = true;


            XSSFCellStyle header1 = (XSSFCellStyle)workbook.CreateCellStyle();
            header1.Alignment = HorizontalAlignment.Left;
            header1.VerticalAlignment = VerticalAlignment.Center;
            header1.BorderBottom = BorderStyle.Thin;
            header1.BorderLeft = BorderStyle.Thin;
            header1.BorderRight = BorderStyle.Thin;
            header1.BorderTop = BorderStyle.Thin;
            header1.SetFont(font12Bold);
            header1.DataFormat = simpleDataFormat;

            styles.Add(header1);

            XSSFCellStyle header2 = (XSSFCellStyle)workbook.CreateCellStyle();
            header2.CloneStyleFrom(header1);
            header2.SetFont(font10);

            styles.Add(header2);

            XSSFCellStyle header3 = (XSSFCellStyle)workbook.CreateCellStyle();
            header3.CloneStyleFrom(header1);
            header3.Alignment = HorizontalAlignment.Center;
            header3.SetFont(font10Bold);

            styles.Add(header3);

            XSSFCellStyle tableColumn = (XSSFCellStyle)workbook.CreateCellStyle();
            tableColumn.CloneStyleFrom(header1);
            tableColumn.Alignment = HorizontalAlignment.Center;
            tableColumn.SetFont(font10);

            styles.Add(tableColumn);

            XSSFCellStyle tableColumnGrey = (XSSFCellStyle)workbook.CreateCellStyle();
            tableColumnGrey.CloneStyleFrom(header1);
            tableColumnGrey.Alignment = HorizontalAlignment.Center;
            tableColumnGrey.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            tableColumnGrey.FillPattern = FillPattern.SolidForeground;
            tableColumnGrey.SetFont(font10);

            styles.Add(tableColumnGrey);

            XSSFCellStyle pracaColumn = (XSSFCellStyle)workbook.CreateCellStyle();
            pracaColumn.CloneStyleFrom(header1);
            pracaColumn.Alignment = HorizontalAlignment.Left;
            pracaColumn.SetFont(font10);

            styles.Add(pracaColumn);

            XSSFCellStyle pracaColumnGrey = (XSSFCellStyle)workbook.CreateCellStyle();
            pracaColumnGrey.CloneStyleFrom(header1);
            pracaColumnGrey.Alignment = HorizontalAlignment.Left;
            pracaColumnGrey.FillForegroundColor = IndexedColors.Grey25Percent.Index;
            pracaColumnGrey.FillPattern = FillPattern.SolidForeground;
            pracaColumnGrey.SetFont(font10);

            styles.Add(pracaColumnGrey);


            return styles;
        }

        //Get Midia to Download 
        private MidiaContentViewModel GetMidia(IWorkbook wb)
        {
            using var stream = new MemoryStream();
            wb.Write(stream);

            var midia = new MidiaContentViewModel
            {
                Content = stream.ToArray(),
                Name = GetFileName(),
                ContentType = MimeTypeHelper.Excel
            };

            return midia;
        }

        //Get File Name (Function)
        private string GetFileName() => $"RelatorioCompras_{DateTime.Now:yyyyMMddHHmm}.xlsx";
        //Report text info
        private string ReportTextInfo() => $"Relatório de Compras para Estudos";
        //Get Lote Cell 
        private string LoteCellText(RCompraCabecalhoViewModel cabecalho)
        {
            return $"Empresa: {cabecalho.EmpresaNome} | Data Impressão: {cabecalho.DataImpressao:dd/MM/yyyy}";
        }

        //Crete empty columns 
        private void CreateNoDataFoundCells(ICellStyle cellStyle, ISheet sheet, int columnQtt, int startRowIndex)
        {
            int rowIndex = startRowIndex;

            var style = (XSSFCellStyle)sheet.Workbook.CreateCellStyle();
            style.CloneStyleFrom(cellStyle);
            style.FillForegroundColor = IndexedColors.Grey25Percent.Index;

            IRow row1 = sheet.CreateRow(rowIndex++);
            IRow row2 = sheet.CreateRow(rowIndex);

            _ = ExcelUtils.CreateCell(row1, style, 0, "NaoHaInformacoes");
            _ = ExcelUtils.CreateCell(row2, style, 0, null);

            for (int i = 1; i < columnQtt; i++)
            {
                _ = ExcelUtils.CreateCell(row1, style, i, null);
                _ = ExcelUtils.CreateCell(row2, style, i, null);
            }

            sheet.AddMergedRegion(new CellRangeAddress(startRowIndex, rowIndex, 0, columnQtt - 1));
        }

        //Login cell text 
        private string LoginCellText(RCompraCabecalhoViewModel cabecalho)
        {
            return $"Login: {cabecalho.UsuarioLogin} | Nome: {cabecalho.UsuarioNome}";
        }

        //Crete Table Columns
        private void CreateTableColumns(ISheet sheet, List<List<GroupColumnConfig>> rowGroups, int startColIndex)
        {
            foreach (var group in rowGroups)
            {
                int cellIndexAux = startColIndex;
                foreach (var cell in group)
                {
                    int startCellIndex = cellIndexAux;
                    int endCellIndex = cellIndexAux + (cell.ColumnQtt - 1);
                    cellIndexAux = endCellIndex + 1;

                    for (int i = startCellIndex; i <= endCellIndex; i++)
                        _ = ExcelUtils.CreateCell(cell.Row, cell.CellStyle, i, (i == startCellIndex ? cell.Desc : null));

                    if (cell.MergeColumns)
                    {
                        if (cell.IsCustomMerge && cell.MergeConfig != null)
                            sheet.AddMergedRegion(new CellRangeAddress(cell.MergeConfig.RowStart, cell.MergeConfig.RowEnd, cell.MergeConfig.ColumnStart, cell.MergeConfig.ColumnEnd));
                        else
                            sheet.AddMergedRegion(new CellRangeAddress(cell.Row.RowNum, cell.Row.RowNum, startCellIndex, endCellIndex));
                    }
                }
            }
        }

        public static XSSFColor StringToByteArray(string hexString)
        {
            hexString = hexString.Replace("#", "");

            var hex = Enumerable.Range(0, hexString.Length)
                         .Where(x => x % 2 == 0)
                         .Select(x => Convert.ToByte(hexString.Substring(x, 2), 16))
                         .ToArray();

            return new XSSFColor(hex);
        }

        //Set Color by atraso
        public static XSSFCellStyle SetColorColumnByAtraso(string colorHex, XSSFCellStyle tableColumn, IWorkbook workbook)
        {
            XSSFCellStyle style = (XSSFCellStyle)workbook.CreateCellStyle();
            style.CloneStyleFrom(tableColumn);
            style.SetFillForegroundColor(StringToByteArray(colorHex));
            style.FillPattern = FillPattern.SolidForeground;

            return style;
        }

        //Zip File
        public static byte[] GetZipArchive(List<MidiaContentViewModel> files)
        {
            byte[] archiveFile;
            using (var archiveStream = new MemoryStream())
            {
                using (var archive = new ZipArchive(archiveStream, ZipArchiveMode.Create, true))
                {
                    foreach (var file in files)
                    {
                        var zipArchiveEntry = archive.CreateEntry(file.Name, CompressionLevel.Optimal);
                        using (var zipStream = zipArchiveEntry.Open())
                            zipStream.Write(file.Content, 0, file.Content.Length);
                    }
                }

                archiveFile = archiveStream.ToArray();
            }

            return archiveFile;
        }
    }
}
