﻿using System;
using System.Collections.Generic;

namespace Zeus.ContentTypes
{
	public interface IContentTypeManager
	{
		ContentType this[Type type]
		{
			get;
		}

		ContentType this[string discriminator]
		{
			get;
		}

		ContentItem CreateInstance(Type itemType, ContentItem parentItem);
		ICollection<ContentType> GetDefinitions();
	}
}