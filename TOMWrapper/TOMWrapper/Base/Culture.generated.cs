// Code generated by a template
using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using TabularEditor.PropertyGridUI;
using TabularEditor.UndoFramework;
using TOM = Microsoft.AnalysisServices.Tabular;

namespace TabularEditor.TOMWrapper
{
  
    /// <summary>
	/// Base class declaration for Culture
	/// </summary>
	[TypeConverter(typeof(DynamicPropertyConverter))]
	public partial class Culture: TabularNamedObject
			, IAnnotationObject
			, IClonableObject
	{
	    protected internal new TOM.Culture MetadataObject { get { return base.MetadataObject as TOM.Culture; } internal set { base.MetadataObject = value; } }

		public string GetAnnotation(string name) {
		    return MetadataObject.Annotations.Find(name)?.Value;
		}
		public void SetAnnotation(string name, string value, bool undoable = true) {
			if(MetadataObject.Annotations.Contains(name)) {
				MetadataObject.Annotations[name].Value = value;
			} else {
				MetadataObject.Annotations.Add(new TOM.Annotation{ Name = name, Value = value });
			}
			if (undoable) Handler.UndoManager.Add(new UndoAnnotationAction(this, name, value));
		}
		


		/// <summary>
		/// Creates a new Culture and adds it to the parent Model.
		/// </summary>
		public Culture(Model parent, string name = null) : this(new TOM.Culture()) {
			
			MetadataObject.Name = GetNewName(parent.MetadataObject.Cultures, string.IsNullOrWhiteSpace(name) ? "New Culture" : name);

			parent.Cultures.Add(this);
		}

		
		public Culture() : this(TabularModelHandler.Singleton.Model) { }


		/// <summary>
		/// Creates an exact copy of this Culture object.
		/// </summary>
		/// 
		public Culture Clone(string newName = null) {
		    Handler.BeginUpdate("Clone Culture");

				// Create a clone of the underlying metadataobject:
				var tom = MetadataObject.Clone() as TOM.Culture;

				// Assign a new, unique name:
				tom.Name = Parent.Cultures.MetadataObjectCollection.GetNewName(string.IsNullOrEmpty(newName) ? tom.Name + " copy" : newName);
				
				// Create the TOM Wrapper object, representing the metadataobject:
				var obj = new Culture(tom);

				// Add the object to the parent collection:
				Parent.Cultures.Add(obj);



            Handler.EndUpdate();

            return obj;
		}

		TabularNamedObject IClonableObject.Clone(string newName, bool includeTranslations, TabularNamedObject newParent) 
		{
			if (includeTranslations) throw new ArgumentException("This object does not support translations", "includeTranslations");
			if (newParent != null) throw new ArgumentException("This object can not be cloned to another parent. Argument newParent should be left as null.", "newParent");
			return Clone(newName);
		}

	
        internal override void RenewMetadataObject()
        {
            var tom = new TOM.Culture();
            Handler.WrapperLookup.Remove(MetadataObject);
            MetadataObject.CopyTo(tom);
            MetadataObject = tom;
            Handler.WrapperLookup.Add(MetadataObject, this);
        }


		public Model Parent { 
			get {
				return Handler.WrapperLookup[MetadataObject.Parent] as Model;
			}
		}

		/// <summary>
		/// Creates a Culture object representing an existing TOM Culture.
		/// </summary>
		internal Culture(TOM.Culture metadataObject) : base(metadataObject)
		{
		}	

		public override bool Browsable(string propertyName) {
			switch (propertyName) {
				case "Parent":
					return false;
				
				default:
					return base.Browsable(propertyName);
			}
		}

    }


	/// <summary>
	/// Collection class for Culture. Provides convenient properties for setting a property on multiple objects at once.
	/// </summary>
	public partial class CultureCollection: TabularObjectCollection<Culture, TOM.Culture, TOM.Model>
	{
		public Model Parent { get; private set; }

		public CultureCollection(string collectionName, TOM.CultureCollection metadataObjectCollection, Model parent) : base(collectionName, metadataObjectCollection)
		{
			Parent = parent;

			// Construct child objects (they are automatically added to the Handler's WrapperLookup dictionary):
			foreach(var obj in MetadataObjectCollection) {
				new Culture(obj) { Collection = this };
			}
		}


		public override string ToString() {
			return string.Format("({0} {1})", Count, (Count == 1 ? "Culture" : "Cultures").ToLower());
		}
	}
}
