﻿namespace DP1.Tests
{
    public class FileParserTestInputData
    {
        public const string SmallInput = @"
# Nodes:
IN: INPUT_HIGH;
NOTNODE: NOT;
OUT: PROBE;

# EDGES:
IN: NOTNODE;
NOTNODE: OUT;";

        public const string MainInputFile = @"
# Circuit1.txt
#
# Full-adder. Deze file bevat een correct circuit
#
#
#
# Description of all the nodes
#
A: INPUT_HIGH;
B: INPUT_HIGH;
Cin: INPUT_LOW;
Cout: PROBE;
S: PROBE;
NODE1: OR;
NODE2: AND;
NODE3: AND;
NODE4: NOT;
NODE5: AND;
NODE6: OR;
NODE7: NOT;
NODE8: NOT;
NODE9: AND;
NODE10: AND;
NODE11: OR;
#
#
# Description of all the edges
#
Cin: NODE3,NODE7,NODE10;
A: NODE1,NODE2;
B: NODE1,NODE2;
NODE1: NODE3,NODE5;
NODE2: NODE4,NODE6;
NODE3: NODE6;
NODE4: NODE5;
NODE5: NODE8,NODE9;
NODE6: Cout;
NODE7: NODE9;
NODE8: NODE10;
NODE9: NODE11;
NODE10: NODE11;
NODE11: S;";
    }
}
