﻿using System;
using SoundInTheory.NMigration.Framework;
using Smo = Microsoft.SqlServer.Management.Smo;

namespace Zeus.Installation.Migrations
{
	[Migration(1)]
	public class Migration001 : Migration
	{
		public override void Up()
		{
			AddTable("zeusItems",
				new Column("ID", Smo.DataType.Int, ColumnProperties.PrimaryKeyWithIdentity),
				new Column("Type", Smo.DataType.NVarChar(500), ColumnProperties.NotNull),
				new Column("Created", Smo.DataType.DateTime, ColumnProperties.NotNull),
				new Column("Published", Smo.DataType.DateTime, ColumnProperties.Null),
				new Column("Updated", Smo.DataType.DateTime, ColumnProperties.NotNull),
				new Column("Expires", Smo.DataType.DateTime, ColumnProperties.Null),
				new Column("Name", Smo.DataType.NVarChar(250), ColumnProperties.Null),
				new Column("ZoneName", Smo.DataType.NVarChar(50), ColumnProperties.Null),
				new Column("Title", Smo.DataType.NVarChar(250), ColumnProperties.Null),
				new Column("SortOrder", Smo.DataType.Int, ColumnProperties.NotNull),
				new Column("Visible", Smo.DataType.Bit, ColumnProperties.NotNull),
				new Column("SavedBy", Smo.DataType.NVarChar(50), ColumnProperties.Null),
				new Column("VersionOfID", Smo.DataType.Int, ColumnProperties.Null),
				new Column("ParentID", Smo.DataType.Int, ColumnProperties.Null));

			AddTable("zeusDetailCollections",
				new Column("ID", Smo.DataType.Int, ColumnProperties.PrimaryKeyWithIdentity),
				new Column("ItemID", Smo.DataType.Int, ColumnProperties.Null),
				new Column("Name", Smo.DataType.NVarChar(50), ColumnProperties.NotNull));

			AddTable("zeusAuthorizedRoles",
				new Column("ID", Smo.DataType.Int, ColumnProperties.PrimaryKeyWithIdentity),
				new Column("ItemID", Smo.DataType.Int, ColumnProperties.NotNull),
				new Column("Role", Smo.DataType.NVarChar(50), ColumnProperties.NotNull));

			AddTable("zeusDetails",
				new Column("ID", Smo.DataType.Int, ColumnProperties.PrimaryKeyWithIdentity),
				new Column("Type", Smo.DataType.NVarChar(250), ColumnProperties.NotNull),
				new Column("ItemID", Smo.DataType.Int, ColumnProperties.NotNull),
				new Column("DetailCollectionID", Smo.DataType.Int, ColumnProperties.Null),
				new Column("Name", Smo.DataType.NVarChar(50), ColumnProperties.Null),
				new Column("BoolValue", Smo.DataType.Bit, ColumnProperties.Null),
				new Column("IntValue", Smo.DataType.Int, ColumnProperties.Null),
				new Column("LinkValue", Smo.DataType.Int, ColumnProperties.Null),
				new Column("DoubleValue", Smo.DataType.Float, ColumnProperties.Null),
				new Column("DateTimeValue", Smo.DataType.DateTime, ColumnProperties.Null),
				new Column("StringValue", Smo.DataType.NVarCharMax, ColumnProperties.Null),
				new Column("Value", Smo.DataType.VarBinaryMax, ColumnProperties.Null));

			AddForeignKey("zeusDetailCollections", "ItemID", "zeusItems", "ID");
			AddForeignKey("zeusAuthorizedRoles", "ItemID", "zeusItems", "ID");
			AddForeignKey("zeusDetails", "ItemID", "zeusItems", "ID");
			AddForeignKey("zeusDetails", "DetailCollectionID", "zeusDetailCollections", "ID");
		}

		public override void Down()
		{
			RemoveTable("zeusDetails");
			RemoveTable("zeusAuthorizedRoles");
			RemoveTable("zeusDetailCollections");
			RemoveTable("zeusItems");
		}
	}
}