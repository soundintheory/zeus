﻿using System;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Zeus.Web.UI.WebControls;

namespace Zeus.ContentTypes.Properties
{
	public abstract class AbstractEditorAttribute : Attribute, IEditor
	{
		private string _requiredText, _requiredErrorMessage;

		#region Properties

		/// <summary>Gets or sets the name of the detail (property) on the content item's object.</summary>
		public string Name
		{
			get;
			set;
		}

		/// <summary>Gets or sets the order of the associated control</summary>
		public int SortOrder
		{
			get;
			set;
		}

		/// <summary>Gets or sets the label used for presentation.</summary>
		public string Title
		{
			get;
			set;
		}

		public string ContainerName
		{
			get;
			set;
		}

		public bool Required
		{
			get;
			set;
		}

		public string Description
		{
			get;
			set;
		}

		public bool IsLocallyUnique
		{
			get;
			set;
		}

		public string RequiredText
		{
			get { return _requiredText ?? "&nbsp;*"; }
			set { _requiredText = value; }
		}

		public string RequiredErrorMessage
		{
			get { return _requiredErrorMessage ?? string.Format("{0} is required", this.Title); }
			set { _requiredErrorMessage = value; }
		}

		#endregion

		#region Constructors

		/// <summary>Default/empty constructor.</summary>
		public AbstractEditorAttribute()
		{
			
		}

		/// <summary>Initializes a new instance of the AbstractEditableAttribute.</summary>
		/// <param name="title">The label displayed to editors</param>
		/// <param name="name">The name used for equality comparison and reference.</param>
		/// <param name="sortOrder">The order of this editor</param>
		public AbstractEditorAttribute(string title, int sortOrder)
		{
			this.Title = title;
			this.SortOrder = sortOrder;
		}

		/// <summary>Initializes a new instance of the AbstractEditableAttribute.</summary>
		/// <param name="title">The label displayed to editors</param>
		/// <param name="name">The name used for equality comparison and reference.</param>
		/// <param name="sortOrder">The order of this editor</param>
		public AbstractEditorAttribute(string title, string name, int sortOrder)
		{
			this.Title = title;
			this.Name = name;
			this.SortOrder = sortOrder;
		}

		#endregion

		#region Methods

		public virtual Control AddTo(Control container)
		{
			Control panel = AddPanel(container);
			Label label = AddLabel(panel);
			Control editor = AddEditor(panel);
			if (label != null && editor != null && !string.IsNullOrEmpty(editor.ID))
				label.AssociatedControlID = editor.ID;
			if (this.Required)
				AddRequiredFieldValidator(panel, editor);
			if (this.IsLocallyUnique)
				AddLocallyUniqueValidator(panel, editor);
			if (!string.IsNullOrEmpty(this.Description))
				container.Controls.Add(new LiteralControl("<span class=\"description\">" + this.Description + "</span>"));

			return editor;
		}

		/// <summary>Adds the panel to the container. Creating this panel and adding labels and editors to it will help to avoid web controls from interfering with each other.</summary>
		/// <param name="container">The container onto which add the panel.</param>
		/// <returns>A panel that can be used to add editor and label.</returns>
		protected virtual Control AddPanel(Control container)
		{
			HtmlGenericControl detailContainer = new HtmlGenericControl("div");
			detailContainer.Attributes["class"] = "editDetail";
			container.Controls.Add(detailContainer);
			return detailContainer;
		}

		/// <summary>Adds a label with the text set to the current Title to the container.</summary>
		/// <param name="container">The container control for the label.</param>
		protected virtual Label AddLabel(Control container)
		{
			Label label = new Label();
			label.ID = "lbl" + this.Name;
			label.Text = this.Title;
			label.CssClass = "editorLabel";
			container.Controls.Add(label);
			return label;
		}

		/// <summary>Adds the editor control to the edit panel. This method is invoked by <see cref="AddTo"/> and the editor is prepended a label and wrapped in a panel. To remove these controls also override the <see cref="AddTo"/> method.</summary>
		/// <param name="container">The container onto which to add the editor.</param>
		/// <returns>A reference to the added editor.</returns>
		protected abstract Control AddEditor(Control container);

		protected virtual IValidator AddRequiredFieldValidator(Control container, Control editor)
		{
			RequiredFieldValidator rfv = new RequiredFieldValidator();
			rfv.ID = "rfv" + this.Name;
			rfv.ControlToValidate = editor.ID;
			rfv.Display = ValidatorDisplay.Dynamic;
			rfv.Text = this.RequiredText;
			rfv.ErrorMessage = this.RequiredErrorMessage;
			container.Controls.Add(rfv);

			return rfv;
		}

		protected virtual IValidator AddLocallyUniqueValidator(Control container, Control editor)
		{
			LocallyUniqueValidator rfv = new LocallyUniqueValidator();
			rfv.ID = "luv" + this.Name;
			rfv.ControlToValidate = editor.ID;
			rfv.Display = ValidatorDisplay.Dynamic;
			rfv.DisplayName = this.Title;
			rfv.PropertyName = this.Name;
			rfv.Text = "*";
			rfv.ErrorMessage = "*";
			container.Controls.Add(rfv);

			return rfv;
		}

		/// <summary>Compares two values regarding null values as equal.</summary>
		protected bool AreEqual(object editorValue, object itemValue)
		{
			return (editorValue == null && itemValue == null)
						 || (editorValue != null && editorValue.Equals(itemValue))
						 || (itemValue != null && itemValue.Equals(editorValue));
		}

		/// <summary>Updates the object with the values from the editor.</summary>
		/// <param name="item">The object to update.</param>
		/// <param name="editor">The editor contorl whose values to update the object with.</param>
		/// <returns>True if the item was changed (and needs to be saved).</returns>
		public abstract bool UpdateItem(ContentItem item, Control editor);

		/// <summary>Updates the editor with the values from the item.</summary>
		/// <param name="item">The item that contains values to assign to the editor.</param>
		/// <param name="editor">The editor to load with a value.</param>
		public abstract void UpdateEditor(ContentItem item, Control editor);

		#endregion
	}
}
