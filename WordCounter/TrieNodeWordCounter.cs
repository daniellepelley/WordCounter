namespace WordCounter
{
    public class TrieNodeWordCounter : IWordCounter
    {
        private readonly TrieNode _node = new TrieNode(null, (char)10);

        public void AddWord(string word)
        {
            _node.AddWord(word);
        }
    }
}