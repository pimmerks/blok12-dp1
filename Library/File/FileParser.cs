using System;
using System.Collections.Generic;
using System.Linq;

namespace DP1.Library.File
{
    using System.IO;
    using Exceptions;

    public class FileParser
    {
        private const char CommentChar = '#';

        /// <summary>
        /// Loads a file and returns all the lines.
        /// </summary>
        /// <param name="path">The path of the file.</param>
        /// <returns>A list of lines.</returns>
        public List<string> ReadFileLines(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"File {path} does not exist!", path);
            }

            return File.ReadAllLines(path).ToList();
        }

        /// <summary>
        /// Parse multiple lines and return the <see cref="NodeDefinition"/>s and <see cref="NodeConnectionDefinition"/>.
        /// </summary>
        /// <param name="lines">The lines to parse.</param>
        public (List<NodeDefinition> nodeDefinitions,
                List<NodeConnectionDefinition> nodeConnectionDefinitions)
            ParseLines(List<string> lines)
        {
            var parsedText = lines.Select(this.ParseLine).ToList();
            var nodeDefinitions =
                parsedText
                    .OfType<NodeDefinition>()
                    .ToList();

            var connectionDefinitions =
                parsedText
                    .OfType<NodeConnectionDefinition>()
                    .ToList();

            // TODO: Duplicate entries?

            return (nodeDefinitions, connectionDefinitions);
        }

        /// <summary>
        /// Parses one line.
        /// </summary>
        /// <param name="line">The line to parse.</param>
        /// <returns>An instance of <see cref="ParsedLine"/>.</returns>
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
                throw new LineParseException("Line must contain ':' and ';'", trimmed);
            }

            var split = trimmed.Split(':');
            if (split.Count() != 2)
            {
                throw new LineParseException("Line can only contain 1 ':'", trimmed);
            }

            var p1 = split[0].Trim();
            var p2 = split[1].Trim().Split(';')[0];

            if (string.IsNullOrWhiteSpace(p1) || string.IsNullOrWhiteSpace(p2))
            {
                throw new LineParseException("One or more of the variables are empty.", trimmed);
            }

            if (ParsedLine.NodeTypes.Contains(p2))
            {
                return new NodeDefinition(p1, p2);
            }

            var p2Split = p2.Split(',');
            if (p2Split.Any(string.IsNullOrWhiteSpace))
            {
                throw new LineParseException("One or more of the output nodes is empty.", trimmed);
            }

            return new NodeConnectionDefinition(p1, p2.Split(',').ToList());
        }
    }
}
