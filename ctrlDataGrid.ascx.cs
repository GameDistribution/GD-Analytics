using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class analitcs_ctrlDataGrid : System.Web.UI.UserControl
{
    private string _Title;
    public string Title { 
        get {
            return _Title;
        }

        set
        {
            _Title = value;
        }
    }

    private string _TableName;
    public string TableName
    {
        get
        {
            return _TableName;
        }

        set
        {
            _TableName = value;
        }
    }

    private string _TableColumnNames;
    public string TableColumnNames
    {
        get
        {
            return _TableColumnNames;
        }

        set
        {
            _TableColumnNames = value;
        }
    }

    public string[] TableColumn;

    private bool _TablePager;
    public bool TablePager
    {
        get
        {
            return _TablePager;
        }

        set
        {
            _TablePager = value;
        }
    }

    private string _TableColumnWidths;
    public string TableColumnWidths
    {
        get
        {
            return _TableColumnWidths;
        }

        set
        {
            _TableColumnWidths = value;
        }
    }

    public string[] TableColumnWidth;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    public string getColumns()
    {
        string htmlCols = "";

        TableColumn = TableColumnNames.Split(',');

        if (TableColumnWidths!=null & TableColumnWidths != "") TableColumnWidth = TableColumnWidths.Split(',');

        int i = 0;
        foreach (string colname in TableColumn) 
        {
            if (TableColumnWidths != null & TableColumnWidths != "")
            {
                htmlCols = String.Concat(htmlCols, @"
                <th style=""text-align: center;"" width=""", TableColumnWidth[i], @""">
                    ", TableColumn[i], @"
                </th>");
            }
            else
            {
                htmlCols = String.Concat(htmlCols, @"
                <th style=""text-align: center;"" width=""", (i == 0 ? 65 : (int)30 / TableColumn.Count()), @"%"">
                    ", TableColumn[i], @"
                </th>");
            }
            i++;
        }
        return htmlCols;
    }
}