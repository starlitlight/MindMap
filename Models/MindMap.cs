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

        private ICollection<Node> _nodes;
        public virtual ICollection<Node> Nodes 
        { 
            get => _nodes ?? (_nodes = new List<Node>());
            set => _nodes = value;
        }
    }

    public class Node
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid? ParentNodeId { get; set; }

        public virtual Node ParentNode { get; set; }
        
        private ICollection<Node> _childNodes;
        public virtual ICollection<Node> ChildNodes 
        { 
            get => _childNodes ?? (_childNodes = new List<Node>());
            set => _childNodes = value;
        }

        [Required]
        public Guid MindMapId { get; set; }

        public virtual MindMap MindMap { get; set; }
    }
}