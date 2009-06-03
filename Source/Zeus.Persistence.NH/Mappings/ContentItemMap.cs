﻿using FluentNHibernate.Mapping;
using Zeus.ContentProperties;
using Zeus.Security;

namespace Zeus.Persistence.NH.Mappings
{
	public class ContentItemMap : ClassMap<ContentItem>
	{
		public ContentItemMap()
		{
			WithTable("zeusItems");

			Cache.AsReadWrite();

			Id(x => x.ID).WithUnsavedValue(0).Access.AsProperty().GeneratedBy.Native();

			DiscriminateSubClassesOnColumn("Type")
				.SubClass<ContentItem>("ContentItem", m => m.Map(x => x.ZoneName).WithLengthOf(50));

			// Add subclasses

			Map(x => x.Created).Not.Nullable();
			Map(x => x.Published);
			Map(x => x.Updated).Not.Nullable();
			Map(x => x.Expires);

			Map(x => x.Name).WithLengthOf(250);
			Map(x => x.Title).WithLengthOf(250);
			Map(x => x.SortOrder).Not.Nullable();
			Map(x => x.Visible).Not.Nullable();
			Map(x => x.SavedBy).WithLengthOf(50);

			References(x => x.VersionOf, "VersionOfID").LazyLoad().FetchType.Select();
			References(x => x.Parent, "ParentID").LazyLoad().FetchType.Select();

			HasMany(x => x.Children)
				.AsBag()
				.Cascade.All()
				.Inverse()
				.LazyLoad()
				.WithKeyColumn("ParentID")
				// cache usage=read-write
				.SetAttribute("order-by", "SortOrder");

			HasMany<PropertyData>(x => x.Details)
				//.AsList(cd => cd.WithColumn("ID"))
				.AsBag()
				.Inverse()
				.Cascade.AllDeleteOrphan()
				.LazyLoad()
				// cache usage=read-write
				.WithKeyColumn("ItemID")
				.Where("DetailCollectionID IS NULL");

			HasMany<PropertyCollection>(x => x.DetailCollections)
				.AsMap(cd => cd.Name)
				.Inverse()
				.Cascade.AllDeleteOrphan()
				.LazyLoad()
				.WithKeyColumn("ItemID")
				// cache usage=read-write
				;

			HasMany(x => x.AuthorizationRules)
				.AsBag()
				.Cascade.AllDeleteOrphan()
				.Inverse()
				.LazyLoad()
				.WithKeyColumn("ItemID")
				// cache usage=read-write
				.FetchType.Join();
		}
	}
}