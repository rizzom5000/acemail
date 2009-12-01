/*Checkbox Functionality Provided By Microsoft MSDN Magazine*/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

namespace CustomControls
{
    public class CustomGridView : GridView
    {
        private const string CGV_JS = "CustomControls.CustomGridView.js";
        private ArrayList selectedIndices;

        public CustomGridView()
        { }

        #region Properties
        // PROPERTY:: AutoGenerateCheckBoxColumn
        [Category("Behavior")]
        [Description("Whether a checkbox column is generated automatically at runtime")]
        [DefaultValue(false)]
        public bool AutoGenerateCheckBoxColumn
        {
            get
            {
                object o = ViewState["AutoGenerateCheckBoxColumn"];
                if (o == null)
                    return false;
                return (bool)o;
            }
            set { ViewState["AutoGenerateCheckBoxColumn"] = value; }
        }

        // PROPERTY:: CheckBoxColumnIndex
        [Category("Behavior")]
        [Description("Indicates the 0-based position of the checkbox column")]
        [DefaultValue(0)]
        public int CheckBoxColumnIndex
        {
            get
            {
                object o = ViewState["CheckBoxColumnIndex"];
                if (o == null)
                    return 0;
                return (int)o;
            }
            set
            {
                ViewState["CheckBoxColumnIndex"] = (value < 0 ? 0 : value);
            }
        }

        // PROPERTY:: SelectedIndices
        internal virtual ArrayList SelectedIndices
        {
            get
            {
                selectedIndices = new ArrayList();
                for (int i = 0; i < Rows.Count; i++)
                {
                    // Retrieve the reference to the checkbox
                    CheckBox cb = (CheckBox)Rows[i].FindControl(InputCheckBoxField.CheckBoxID);
                    if (cb == null)
                        return selectedIndices;
                    if (cb.Checked)
                        selectedIndices.Add(i);
                }
                return selectedIndices;
            }
        }

        // METHOD:: GetSelectedIndices
        public virtual int[] GetSelectedIndices()
        {
            return (int[])SelectedIndices.ToArray(typeof(int));
        }

        //mouse hover controls
        public bool MouseHoverRowHighlightEnabled
        {
            get
            {
                if (ViewState["MouseHoverRowHighlightEnabled"] != null)
                    return (bool)ViewState["MouseHoverRowHighlightEnabled"];
                else
                    return false;
            }
            set { ViewState["MouseHoverRowHighlightEnabled"] = value; }
        }

        public Color RowHighlightColor
        {
            get
            {
                if (ViewState["RowHighlightColor"] != null)
                    return (Color)ViewState["RowHighlightColor"];
                else
                {
                    return Color.Yellow;
                }
            }
            set { ViewState["RowHighlightColor"] = value; }
        }

        //customized row features
        protected override void OnRowCreated(GridViewRowEventArgs e)
        {
            base.OnRowCreated(e);
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", string.Format("this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='{0}'", ColorTranslator.ToHtml(RowHighlightColor)));
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");
                //e.Row.Attributes["OnDblClick"] = Page.ClientScript.GetPostBackEventReference(this, "Select$" + e.Row.RowIndex);
            }
        }
        #endregion

        #region Members overrides
        // METHOD:: CreateColumns
        protected override ICollection CreateColumns(PagedDataSource dataSource, bool useDataSource)
        {
            // Let the GridView create the default set of columns
            ICollection columnList = base.CreateColumns(dataSource, useDataSource);
            if (!AutoGenerateCheckBoxColumn)
                return columnList;
            // Add a checkbox column if required
            ArrayList extendedColumnList = AddCheckBoxColumn(columnList);
            return extendedColumnList;
        }

        // METHOD:: OnLoad
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            Type t = this.GetType();
            string url = Page.ClientScript.GetWebResourceUrl(t, CGV_JS);
            if (!Page.ClientScript.IsClientScriptIncludeRegistered(t, CGV_JS))
                Page.ClientScript.RegisterClientScriptInclude(t, CGV_JS, url);
        }

        // METHOD:: OnPreRender
        protected override void OnPreRender(EventArgs e)
        {
            // Do as usual
            base.OnPreRender(e);
            // Adjust each data row
            foreach (GridViewRow r in Rows)
            {
                // Get the appropriate style object for the row
                TableItemStyle style = GetRowStyleFromState(r.RowState);
                // Retrieve the reference to the checkbox
                CheckBox cb = (CheckBox)r.FindControl(InputCheckBoxField.CheckBoxID);
                // Build the ID of the checkbox in the header
                string headerCheckBoxID = String.Format(CheckBoxColumHeaderID, ClientID);
                // Add script code to enable selection
                cb.Attributes["onclick"] = String.Format("ApplyStyle(this, '{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                        ColorTranslator.ToHtml(SelectedRowStyle.ForeColor),
                        ColorTranslator.ToHtml(SelectedRowStyle.BackColor),
                        ColorTranslator.ToHtml(style.ForeColor),
                        ColorTranslator.ToHtml(style.BackColor),
                        (style.Font.Bold ? 700 : 400),
                        headerCheckBoxID);

                // Update the style of the checkbox if checked
                if (cb.Checked)
                {
                    r.BackColor = SelectedRowStyle.BackColor;
                    r.ForeColor = SelectedRowStyle.ForeColor;
                    r.Font.Bold = SelectedRowStyle.Font.Bold;
                }
                else
                {
                    r.BackColor = style.BackColor;
                    r.ForeColor = style.ForeColor;
                    r.Font.Bold = style.Font.Bold;
                }
            }
        }
        #endregion

        #region Constants
        private const string CheckBoxColumHeaderTemplate = "<input type='checkbox' hidefocus='true' id='{0}' name='{0}' {1} onclick='CheckAll(this)'>";
        private const string CheckBoxColumHeaderID = "{0}_HeaderButton";
        #endregion

        // METHOD:: add a brand new checkbox column
        protected virtual ArrayList AddCheckBoxColumn(ICollection columnList)
        {
            // Get a new container of type ArrayList that contains the given collection. 
            // This is required because ICollection doesn't include Add methods
            // For guidelines on when to use ICollection vs IList see Cwalina's blog
            ArrayList list = new ArrayList(columnList);

            // Determine the check state for the header checkbox
            string shouldCheck = "";
            string checkBoxID = String.Format(CheckBoxColumHeaderID, ClientID);
            if (!DesignMode)
            {
                object o = Page.Request[checkBoxID];
                if (o != null)
                {
                    shouldCheck = "checked=\"checked\"";
                }
            }

            // Create a new custom CheckBoxField object 
            InputCheckBoxField field = new InputCheckBoxField();
            field.HeaderText = String.Format(CheckBoxColumHeaderTemplate, checkBoxID, shouldCheck);
            field.ReadOnly = true;

            // Insert the checkbox field into the list at the specified position
            if (CheckBoxColumnIndex > list.Count)
            {
                // If the desired position exceeds the number of columns 
                // add the checkbox field to the right. Note that this check
                // can only be made here because only now we know exactly HOW 
                // MANY columns we're going to have. Checking Columns.Count in the 
                // property setter doesn't work if columns are auto-generated
                list.Add(field);
                CheckBoxColumnIndex = list.Count - 1;
            }
            else
                list.Insert(CheckBoxColumnIndex, field);
            return list;
        }

        // METHOD:: retrieve the style object based on the row state
        protected virtual TableItemStyle GetRowStyleFromState(DataControlRowState state)
        {
            switch (state)
            {
                case DataControlRowState.Alternate:
                    return AlternatingRowStyle;
                case DataControlRowState.Edit:
                    return EditRowStyle;
                case DataControlRowState.Selected:
                    return SelectedRowStyle;
                default:
                    return RowStyle;
            }
        }
    }

    internal sealed class InputCheckBoxField : CheckBoxField
    {
        public const string CheckBoxID = "CheckBoxButton";
        public InputCheckBoxField()
        { }

        /*protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);
            // Add a checkbox anyway, if not done already
            if (cell.Controls.Count == 0)
            {
                CheckBox chk = new CheckBox();
                chk.ID = InputCheckBoxField.CheckBoxID;
                cell.Controls.Add(chk);
            }
        }*/

        protected override void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
        {
            base.InitializeDataCell(cell, rowState);

            // Add a checkbox anyway, if not done already
            if (cell.Controls.Count == 0)
            {
                CheckBox chk = new CheckBox();
                chk.ID = InputCheckBoxField.CheckBoxID;
                chk.Load += new EventHandler(chk_Load);
                cell.Controls.Add(chk);
            }
        }

        void chk_Load(object sender, EventArgs e)
        {
            CheckBox chk = sender as CheckBox;
            if (null != chk)
            {
                string uniqueID = chk.UniqueID;
                string currentVal = HttpContext.Current.Request.Params.Get(uniqueID);
                if (!string.IsNullOrEmpty(currentVal))
                {
                    chk.Checked = true;
                }
            }
        }
    }
}
