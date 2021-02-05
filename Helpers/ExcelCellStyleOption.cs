using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace TemplateApi.Api.Infrastructure.Helpers
{
    public class ExcelCellStyleOption
    {
        public ExcelCellStyleOption(
            HorizontalAlignment horizontalAlignment,
            VerticalAlignment verticalAlignment,
            short fillForegroundColor,
            XSSFColor fontCustomColor = null,
            short fontHeightInPoints = 9,
            bool isFontBold = false,
            bool wrapText = true,
            FillPattern fillPattern = FillPattern.SolidForeground,
            BorderStyle borderTop = BorderStyle.Thin,
            BorderStyle borderRight = BorderStyle.Thin,
            BorderStyle borderBottom = BorderStyle.Thin,
            BorderStyle borderLeft = BorderStyle.Thin)
        {
            HorizontalAlignment = horizontalAlignment;
            VerticalAlignment = verticalAlignment;
            FillForegroundColor = fillForegroundColor;
            FontCustomColor = fontCustomColor;
            FontHeightInPoints = fontHeightInPoints;
            IsFontBold = isFontBold;
            WrapText = wrapText;
            FillPattern = fillPattern;
            BorderTop = borderTop;
            BorderRight = borderRight;
            BorderBottom = borderBottom;
            BorderLeft = borderLeft;
        }

        public short FontHeightInPoints { get; private set; }
        public bool IsFontBold { get; private set; }
        public XSSFColor FontCustomColor { get; private set; }
        public bool WrapText { get; private set; }
        public short FillForegroundColor { get; private set; }
        public FillPattern FillPattern { get; private set; }
        public HorizontalAlignment HorizontalAlignment { get; private set; }
        public VerticalAlignment VerticalAlignment { get; private set; }
        public BorderStyle BorderTop { get; private set; }
        public BorderStyle BorderRight { get; private set; }
        public BorderStyle BorderBottom { get; private set; }
        public BorderStyle BorderLeft { get; private set; }
    }
}
