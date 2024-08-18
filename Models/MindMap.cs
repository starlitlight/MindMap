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

        private ICollection<Node> _nodesCollection;
        public virtual ICollection<Node> Nodes 
        { 
            get => _nodesCollection ?? (_nodesCollection = new List<Node>());
            set => _nodesCollection = value;
        }
    }

    public class Node
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Content { get; set; }

        public Guid? ParentNodeId { get; set; }

        public virtual Node Parent { get; set; }
        
        private ICollection<Node> _childNodesCollection;
        public virtual ICollection<Node> ChildNodes 
        { 
            get => _childNodesCollection ?? (_childNodesCollection = new List<Node>());
            set => _childNodesCollection = value;
        }

        [Required]
        public Guid MindMapId { get; set; }

        public virtual MindMap MindMap { get; set; }
    }
}