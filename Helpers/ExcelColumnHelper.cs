using NPOI.SS.UserModel;

namespace TemplateApi.Api.Infrastructure.Helpers
{
    public class GroupColumnConfig
    {
        public GroupColumnConfig(
            IRow row,
            ICellStyle cellStyle,
            object desc,
            int columnQtt,
            bool mergeColumns = false,
            bool isCustomMerge = false,
            MergeColumnConfig mergeConfig = null)
        {
            Row = row;
            CellStyle = cellStyle;
            Desc = desc;
            ColumnQtt = columnQtt;
            MergeColumns = mergeColumns;
            IsCustomMerge = isCustomMerge;
            MergeConfig = mergeConfig;
        }

        public IRow Row { get; private set; }
        public ICellStyle CellStyle { get; set; }
        public object Desc { get; private set; }
        public int ColumnQtt { get; private set; }
        public bool MergeColumns { get; set; }
        public bool IsCustomMerge { get; set; }
        public MergeColumnConfig MergeConfig { get; set; }
    }

    public class MergeColumnConfig
    {
        public MergeColumnConfig(int rowStart, int rowEnd, int columnStart, int columnEnd)
        {
            RowStart = rowStart;
            RowEnd = rowEnd;
            ColumnStart = columnStart;
            ColumnEnd = columnEnd;
        }

        public int RowStart { get; private set; }
        public int RowEnd { get; private set; }
        public int ColumnStart { get; private set; }
        public int ColumnEnd { get; private set; }
    }
}
