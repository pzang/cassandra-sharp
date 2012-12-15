// Generated by TinyPG v1.3 available at www.codeproject.com

#pragma warning disable 168

using System;
using System.Collections.Generic;

namespace cqlsh.Parser
{
    #region Parser

    public partial class Parser 
    {
        private Scanner scanner;
        private ParseTree tree;
        
        public Parser(Scanner scanner)
        {
            this.scanner = scanner;
        }

        public ParseTree Parse(string input)
        {
            tree = new ParseTree();
            return Parse(input, tree);
        }

        public ParseTree Parse(string input, ParseTree tree)
        {
            scanner.Init(input);

            this.tree = tree;
            ParseStart(tree);
            tree.Skipped = scanner.Skipped;

            return tree;
        }

        private void ParseString(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.String), "String");
            parent.Nodes.Add(node);

            tok = scanner.Scan(TokenType.STRING);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.STRING) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.STRING.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseInteger(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Integer), "Integer");
            parent.Nodes.Add(node);

            tok = scanner.Scan(TokenType.INTEGER);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.INTEGER) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.INTEGER.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseBool(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Bool), "Bool");
            parent.Nodes.Add(node);

            tok = scanner.LookAhead(TokenType.TRUE, TokenType.FALSE);
            switch (tok.Type)
            {
                case TokenType.TRUE:
                    tok = scanner.Scan(TokenType.TRUE);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.TRUE) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.TRUE.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                        return;
                    }
                    break;
                case TokenType.FALSE:
                    tok = scanner.Scan(TokenType.FALSE);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.FALSE) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.FALSE.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                        return;
                    }
                    break;
                default:
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, 0, tok.StartPos, tok.StartPos, tok.Length));
                    break;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseValue(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Value), "Value");
            parent.Nodes.Add(node);

            tok = scanner.LookAhead(TokenType.STRING, TokenType.INTEGER, TokenType.TRUE, TokenType.FALSE);
            switch (tok.Type)
            {
                case TokenType.STRING:
                    ParseString(node);
                    break;
                case TokenType.INTEGER:
                    ParseInteger(node);
                    break;
                case TokenType.TRUE:
                case TokenType.FALSE:
                    ParseBool(node);
                    break;
                default:
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, 0, tok.StartPos, tok.StartPos, tok.Length));
                    break;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseSetProperty(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.SetProperty), "SetProperty");
            parent.Nodes.Add(node);


            
            tok = scanner.Scan(TokenType.IDENTIFIER);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.IDENTIFIER) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.IDENTIFIER.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            
            tok = scanner.Scan(TokenType.ASSIGN);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.ASSIGN) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.ASSIGN.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            
            ParseValue(node);

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseExit(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Exit), "Exit");
            parent.Nodes.Add(node);

            tok = scanner.Scan(TokenType.EXIT);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.EXIT) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.EXIT.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseHelp(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Help), "Help");
            parent.Nodes.Add(node);

            tok = scanner.Scan(TokenType.HELP);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.HELP) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.HELP.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseReset(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Reset), "Reset");
            parent.Nodes.Add(node);

            tok = scanner.Scan(TokenType.RESET);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.RESET) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.RESET.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseCommand(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Command), "Command");
            parent.Nodes.Add(node);

            tok = scanner.LookAhead(TokenType.EXIT, TokenType.IDENTIFIER, TokenType.RESET, TokenType.HELP);
            switch (tok.Type)
            {
                case TokenType.EXIT:
                    ParseExit(node);
                    break;
                case TokenType.IDENTIFIER:
                    ParseSetProperty(node);
                    break;
                case TokenType.RESET:
                    ParseReset(node);
                    break;
                case TokenType.HELP:
                    ParseHelp(node);
                    break;
                default:
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, 0, tok.StartPos, tok.StartPos, tok.Length));
                    break;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseCqlCommand(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.CqlCommand), "CqlCommand");
            parent.Nodes.Add(node);

            tok = scanner.Scan(TokenType.EVERYTHING);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.EVERYTHING) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.EVERYTHING.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }

        private void ParseStart(ParseNode parent)
        {
            Token tok;
            ParseNode n;
            ParseNode node = parent.CreateNode(scanner.GetToken(TokenType.Start), "Start");
            parent.Nodes.Add(node);


            
            tok = scanner.LookAhead(TokenType.BANG, TokenType.EVERYTHING);
            switch (tok.Type)
            {
                case TokenType.BANG:

                    
                    tok = scanner.Scan(TokenType.BANG);
                    n = node.CreateNode(tok, tok.ToString() );
                    node.Token.UpdateRange(tok);
                    node.Nodes.Add(n);
                    if (tok.Type != TokenType.BANG) {
                        tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.BANG.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                        return;
                    }

                    
                    ParseCommand(node);
                    break;
                case TokenType.EVERYTHING:
                    ParseCqlCommand(node);
                    break;
                default:
                    tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found.", 0x0002, 0, tok.StartPos, tok.StartPos, tok.Length));
                    break;
            }

            
            tok = scanner.Scan(TokenType.EOF);
            n = node.CreateNode(tok, tok.ToString() );
            node.Token.UpdateRange(tok);
            node.Nodes.Add(n);
            if (tok.Type != TokenType.EOF) {
                tree.Errors.Add(new ParseError("Unexpected token '" + tok.Text.Replace("\n", "") + "' found. Expected " + TokenType.EOF.ToString(), 0x1001, 0, tok.StartPos, tok.StartPos, tok.Length));
                return;
            }

            parent.Token.UpdateRange(node.Token);
        }


    }

    #endregion Parser
}
#pragma warning restore 168