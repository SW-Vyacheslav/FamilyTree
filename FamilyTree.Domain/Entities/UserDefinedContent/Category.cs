﻿using FamilyTree.Domain.Entities.Tree;
using FamilyTree.Domain.Enums.UserDefinedContent;
using System.Collections.Generic;

namespace FamilyTree.Domain.Entities.UserDefinedContent
{    
    public class Category
    {
        public int Id { get; set; }
               
        public CategoryType CategoryType { get; set; }

        public string Name { get; set; }

        public bool IsDeletable { get; set; }

        public int OrderNumber { get; set; }

        public Person Person { get; set; }

        public List<DataBlock> DataBlocks { get; set; }
    }
}