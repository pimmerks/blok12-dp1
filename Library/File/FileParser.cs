using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DP1.Library.File
{
    public class FileParser
    {
        private const char CommentChar = '#';

        public List<string> ReadLines(string path)
        {
            if (!System.IO.File.Exists(path))
            {
                throw new Exception($"File {path} does not exist!");
            }

            return System.IO.File.ReadAllLines(path).ToList();
        }

        public (List<NodeDefinition> nodeDefinitions,
                List<NodeConnectionDefinition> nodeConnectionDefinitions)
            ParseLines(List<string> text)
        {
            var parsedText = text.Select(this.ParseLine).ToList();
            var nodeDefinitions =
                parsedText.Where(x => x is NodeDefinition)
                .Select(x => x as NodeDefinition)
                .ToList();

            var connectionDefinitions =
                parsedText.Where(x => x is NodeConnectionDefinition)
                .Select(x => x as NodeConnectionDefinition)
                .ToList();

            return (nodeDefinitions, connectionDefinitions);
        }

        public ParsedLine ParseLine(string line)
        {
            var commentPos = line.IndexOf(CommentChar);
            var lineWithoutComments = line;
            if (commentPos > -1)
            {
                lineWithoutComments = line.Remove(commentPos);
            }
            var trimmed = lineWithoutComments.Trim();

            if (string.IsNullOrWhiteSpace(trimmed))
            {
                return null;
            }

            if (!(trimmed.Contains(":") && trimmed.Contains(";")))
            {
                throw new Exception("Line must contain ':' and ';'");
            }

            var split = trimmed.Split(':');
            if (split.Count() != 2)
            {
                throw new Exception("Line can only contain 1 ':'");
            }

            var p1 = split[0].Trim();
            var p2 = split[1].Trim().Split(';')[0];

            if (string.IsNullOrWhiteSpace(p1) || string.IsNullOrWhiteSpace(p2))
            {
                throw new Exception("One or more of the variables are empty.");
            }

            if (ParsedLine.NodeTypes.Contains(p2))
            {
                return new NodeDefinition(p1, p2);
            }

            var p2Split = p2.Split(',');
            foreach (var s in p2Split)
            {
                if (string.IsNullOrWhiteSpace(s))
                {
                    throw new Exception("One or more of the output nodes is empty.");
                }
            }

            return new NodeConnectionDefinition(p1, p2.Split(',').ToList());
        }
    }
}
