using api_dot_net_core.ViewModels.Shared;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Drawing;

namespace TemplateApi.Api.Infrastructure.Helpers
{
    public static class ExcelUtils
    {
        public static void SetExcelAuthor(IWorkbook workbook)
        {
            var workBookProperties = ((XSSFWorkbook)workbook).GetProperties();
            workBookProperties.CoreProperties.Creator = "Writesys Traffic Systems";
        }

        public static void SetExcelPrintSetup(ISheet sheet)
        {
            sheet.Footer.Left = "Daniel Cruz";
            sheet.PrintSetup.Landscape = true;
            sheet.PrintSetup.PaperSize = (short)PaperSize.A4 + 1;
            sheet.IsPrintGridlines = false;
        }

        public static ICell CreateCell(IRow row, ICellStyle cellStyle, int cellIndex, object cellValue)
        {
            var cell = row.CreateCell(cellIndex);

            switch (cellValue)
            {
                case null:
                    cell.SetCellType(CellType.Blank);
                    break;
                case string _:
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(cellValue as string);
                    break;
                case DateTime _:
                    cell.SetCellValue(Convert.ToDateTime(cellValue));
                    break;
                default:
                    cell.SetCellType(CellType.Numeric);
                    cell.SetCellValue(Convert.ToDouble(cellValue));
                    break;
            }

            cell.CellStyle = cellStyle;
            return cell;
        }

        public static ICellStyle CreateCellStyle(IWorkbook workbook, ExcelCellStyleOption styleOption)
        {
            var fontStyle = workbook.CreateFont() as XSSFFont;
            fontStyle.FontHeightInPoints = styleOption.FontHeightInPoints;
            fontStyle.IsBold = styleOption.IsFontBold;

            if (styleOption.FontCustomColor != null)
            {
                fontStyle.SetColor(styleOption.FontCustomColor);
            }

            var cellStyle = workbook.CreateCellStyle();
            cellStyle.SetFont(fontStyle);

            cellStyle.WrapText = styleOption.WrapText;
            cellStyle.FillForegroundColor = styleOption.FillForegroundColor;
            cellStyle.FillPattern = styleOption.FillPattern;

            cellStyle.Alignment = styleOption.HorizontalAlignment;
            cellStyle.VerticalAlignment = styleOption.VerticalAlignment;

            cellStyle.BorderTop = styleOption.BorderTop;
            cellStyle.BorderRight = styleOption.BorderRight;
            cellStyle.BorderBottom = styleOption.BorderBottom;
            cellStyle.BorderLeft = styleOption.BorderLeft;

            return cellStyle;
        }

        public static void InsertImageIntoSheet<T>(ISheet sheet, int colStart, int colEnd, T cabecalho) where T : BaseExcelHeader
        {
            IDrawing patriarch = sheet.CreateDrawingPatriarch();
            //create the anchor
            XSSFClientAnchor anchor = new XSSFClientAnchor(0, 0, 0, 0, colStart, 0, colEnd, 4)
            {
                AnchorType = AnchorType.MoveDontResize
            };
            int imageId = LoadImage(cabecalho.ContentType, cabecalho.Logo, sheet);
            XSSFPicture picture = (XSSFPicture)patriarch.CreatePicture(anchor, imageId);
            picture.LineStyle = LineStyle.DashDotGel;
        }

        public static void InsertImage<T>(ISheet sheet, int colStart, int colEnd, T cabecalho) where T : BaseExcelViewModel
        {
            IDrawing patriarch = sheet.CreateDrawingPatriarch();
            //create the anchor
            XSSFClientAnchor anchor = new XSSFClientAnchor(0, 0, 0, 0, colStart, 0, colEnd, 4)
            {
                AnchorType = AnchorType.MoveDontResize
            };
            int imageId = LoadImage(cabecalho.ContentType, cabecalho.Logo, sheet);
            XSSFPicture picture = (XSSFPicture)patriarch.CreatePicture(anchor, imageId);
            picture.LineStyle = LineStyle.DashDotGel;
        }

        private static int LoadImage(string contentType, byte[] content, ISheet sheet)
        {
            PictureType pictureType = PictureType.JPEG;
            string type = contentType.Replace("image/", string.Empty);

            if (Enum.TryParse(typeof(PictureType), type, true, out _))
            {
                pictureType = (PictureType)Enum.Parse(typeof(PictureType), type, true);
            }

            return sheet.Workbook.AddPicture(content, pictureType);
        }

        public static Color FromHex(string hex)
        {
            FromHex(hex, out var a, out var r, out var g, out var b);
            return Color.FromArgb(a, r, g, b);
        }

        public static void FromHex(string hex, out byte a, out byte r, out byte g, out byte b)
        {
            hex = ToRgbaHex(hex);

            if (hex is null || !uint.TryParse(hex, System.Globalization.NumberStyles.HexNumber, System.Globalization.CultureInfo.InvariantCulture, out uint packedValue))
            {
                throw new ArgumentException("Hexadecimal string is not in the correct format.", nameof(hex));
            }

            a = (byte)(packedValue >> 0);
            r = (byte)(packedValue >> 24);
            g = (byte)(packedValue >> 16);
            b = (byte)(packedValue >> 8);
        }

        private static string ToRgbaHex(string hex)
        {
            hex = hex[0] == '#' ? hex.Substring(1) : hex;

            if (hex.Length == 8)
            {
                return hex;
            }

            if (hex.Length == 6)
            {
                return hex + "FF";
            }

            if (hex.Length < 3 || hex.Length > 4)
            {
                return null;
            }

            char r = hex[0];
            char g = hex[1];
            char b = hex[2];
            char a = hex.Length == 3 ? 'F' : hex[3];

            return new string(new[] { r, r, g, g, b, b, a, a });
        }
    }
}
