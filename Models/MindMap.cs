using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MindMaps.Models
{
    public class MindMap
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        public virtual ICollection<Node> Nodes { get; set; } = new List<Node>();
    }

    public class Node
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid? ParentNodeId { get; set; }

        public virtual Node ParentNode { get; set; }
        
        public virtual ICollection<Node> ChildNodes { get; set; } = new List<Node>();

        [Required]
        public Guid MindMapId { get; set; }

        public virtual MindMap MindMap { get; set; }
    }
}