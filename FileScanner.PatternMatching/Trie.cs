using System.Collections.Generic;

namespace FileScanner.PatternMatching
{
    /// <summary>
    /// An implementation of the non-deterministic state machine used in the
    /// Aho-Corasick pattern matching algorithm.
    /// </summary>
    class Trie
    {
        private class Node
        {
            public Node Fail { set; get; }
            public Dictionary<char, Node> Children { get; private set; }
            public string Match { set; get; }
         
            public Node()
            {
                Fail = null;
                Children = new Dictionary<char, Node>();
                Match = null;
            }

            public Node Next(char c)
            {
                return Children.ContainsKey(c) ? Children[c] : null;
            }

            public Node ConstructNode(char c)
            {
                if (!Children.ContainsKey(c))
                    Children[c] = new Node();

                return Children[c];
            }
        }

        private Node _root = new Node();
        private Node _state;

        private void ConstructTreeNodes(List<string> patterns)
        {
            foreach (var pattern in patterns)
            {
                var node = _root;
                foreach (var c in pattern)
                    node = node.ConstructNode(c);

                // Set match on the last node of tree path representing the pattern
                node.Match = pattern;
            }
        }

        private void SetFailPaths()
        {
            _root.Fail = _root;

            var queue = new Queue<Node>(new []{ _root });
            while (queue.Count > 0)
            {
                var node = queue.Dequeue();
                
                foreach (var pair in node.Children)
                {
                    var lttr = pair.Key;
                    var child = pair.Value;

                    queue.Enqueue(child);

                    var fail = node.Fail;
                    while (fail.Next(lttr) == null && fail != _root)
                        fail = fail.Fail;

                    child.Fail = fail == _root ? _root : fail.Next(lttr);
                }
            }
        }

        /// <summary>
        /// Constructs the tree from given patterns.
        /// </summary>
        /// <param name="patterns">
        /// Patterns from which the tree is constructed.
        /// </param>
        public Trie(List<string> patterns)
        {
            ConstructTreeNodes(patterns);
            SetFailPaths();

            _state = _root;
        }

        /// <summary>
        /// Feeds the tree state with a new character of text.
        /// </summary>
        /// <param name="c">
        /// A character of text in which matches are to be found.
        /// </param>
        /// <returns>
        /// If changing the state causes a match to be found, its value is
        /// returned, otherwise returns null.
        /// </returns>
        public string Feed(char c)
        {
            while (_state.Next(c) == null && _state != _root)
                _state = _state.Fail;

            if (_state.Next(c) != null)
                _state = _state.Next(c);

            return _state.Match;
        }
    }
}
