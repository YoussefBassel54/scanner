using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScannerProject
{
    public partial class Form1 : Form
    {
        string code;
        public Form1()
        {
            InitializeComponent();
        }

        /*states will help in classifying the stuff written
          which will make it easier in getting tokens */

        //STATES
        enum states { START, COMMENT, NUMBER, IDENTIFIER, ASSIGN, DONE, RESERVED, STRING, FUNCTION };
        states state = states.START;

        //RESERVED WORDS
        string[] ResWords = { "int", "float", "string", "read", "write", "repeat", "until", "if", "elseif", "else", "then", "return", "endl", "end" };

        /* the following is a set of boolean methods that help in identifying
           which state the program should be in */

        bool isDigit(char d)
        {
            if (d >= '0' && d <= '9' || d == '.') return true;
            else return false;
        }

        bool isLetter(char l)
        {
            if (l >= 'a' && l <= 'z' || l >= 'A' && l <= 'Z') return true;
            else return false;
        }

        bool isSymbol(char c)
        {
            if ( c == '(' || c == ')' || c == ';' || c == ',' || c == '{' || c == '}') return true;
            else return false;
        }

        bool isArithmatic(char A)
        {
            if (A == '+' || A == '-' || A == '*' || A == '/') return true;
            else return false;
        }

        bool isCondition(char c) 
        {
            if (c == '=' || c == '<' || c == '>') return true;
            else return false;
        }        

        bool isSpace(char s)
        {
            if (s == ' ' || s == '\t' || s == '\n') return true;
            else return false;
        }

      /*  bool isReserved(string R)
        {
            for (int j = 0; j <= ResWords.Length; j++)
                if (R == ResWords[j])
                    return true;
            return false;
        }*/

            /* DO NOT DELETE THIS EMPTY METHOD OR THE PROGRAM WILL CRASH */
        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        /* code for the scan button */
        private void Button1_Click(object sender, EventArgs e)
        {
            code = InputTxt.Text;
            getToken(code += " ");
        }

        public void getToken(string c)
        {
            string token = "";
            bool ResFlag = false;
            int i = 0;

            while (state != states.DONE)
            {
                switch (state)
                {
                    case states.START:
                        if (isSpace(c[i]))
                        {
                            i++;
                            if (i == c.Length)
                                state = states.DONE;
                            else state = states.START;
                        }
                        /*else if(c[i]=='-')
                        {
                            token += c[i];
                            i++;
                            OutputTxt.Text += token + " , error \n";
                            state = states.DONE;
                        }*/
                        else if (isDigit(c[i]))
                        {
                            state = states.NUMBER;
                        }
                        else if (isLetter(c[i]))
                        {
                            state = states.IDENTIFIER;
                        }
                        else if (c[i] == ':')
                        {
                            state = states.ASSIGN;
                        }
                        
                        else if (c[i] == '"')
                        {
                            i++;
                            state = states.STRING;
                        }
                        else if (isSymbol(c[i]))
                        {
                            switch (c[i])
                            {
                                case ';':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , semi colon\n";
                                    token = "";
                                    break;

                                case '(':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , left parenthesis\n";
                                    break;

                                case ')':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , right parenthesis\n";
                                    break;

                                case ',':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , comma \n";
                                    break;
                                case '{':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , left braces\n";
                                    break;
                                case '}':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , left braces\n";
                                    break;
                            }
                            i++;
                            if (i == c.Length)
                                state = states.DONE;
                            else state = states.START;
                        }
                        else if(isArithmatic(c[i]))
                        {
                            switch (c[i])
                            {
                                case '+':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , add operator\n";
                                    break;

                                case '-':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , subtract operator\n";
                                    break;

                                case '/':
                                    if (c[i + 1] == '*')
                                    {
                                        token += c[i];                                        
                                        state = states.COMMENT;
                                    }
                                    else
                                    {
                                        OutputTxt.Text += "lexeme: " + c[i] + " , divide operator\n";
                                    }
                                    break;

                                case '*':
                                    OutputTxt.Text += "lexeme: " + c[i] + " , multiply operator\n";
                                    break;
                            }
                            i++;
                        }
                       /* else if (c[i] == '/')
                        {
                            token += c[i];
                            i++;
                            if (c[i + 1] == '*')
                                state = states.COMMENT;
                        } */
                        else if(isCondition(c[i]))
                        {
                            switch(c[i])
                            {
                                case '<':
                                    {
                                        if (c[i] == '<' && c[i + 1] == '>')
                                        {
                                            OutputTxt.Text += "lexeme: <>" + " , token: NOT equal \n";
                                            i += 2;
                                        }
                                        else
                                        {
                                            OutputTxt.Text += "lexeme: " + c[i] + " , token: Less than\n";
                                        }
                                        break;
                                    }
                                case '>':
                                    {
                                        OutputTxt.Text += "lexeme: " + c[i] + " , token: More than\n";
                                        break;
                                    }
                                case '=':
                                    {
                                        OutputTxt.Text += "lexeme: " + c[i] + " , token: is equal\n";
                                        break;
                                    }
                            }
                            i++;
                        }
                        else if(c[i] == '&' && c[i+1] == '&')
                        {
                            OutputTxt.Text += "lexeme: && , token:  AND operator \n";
                            i += 2;
                        }
                        else if(c[i]=='|' && c[i+1] =='|') 
                        {
                            OutputTxt.Text += "lexeme: || , token:  OR operator \n";
                            i += 2;
                        }
                       /* else if(c[i] == '<' && c[i+1] == '>') 
                        {
                            OutputTxt.Text += c[i] + c[i + 1] + " , NOT equal \n";
                            i += 2;
                        }*/
                        else state = states.DONE;
                        break;

                    case states.COMMENT:
                        if (state == states.COMMENT) 
                        {                            
                            while (c[i] != '*')
                            {
                                token += c[i];
                                i++;
                            }
                            token += c[i];
                            i++;
                            if (c[i] == '/')
                            {
                                token += c[i];
                                OutputTxt.Text += "lexeme: " + token + " , token: comment \n";
                            }
                            else
                            {
                                state = states.COMMENT;
                                break;
                            }
                            
                            token = "";
                            i++;
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                        }
                        break;

                    case states.NUMBER:
                        while (isDigit(c[i]))
                        {
                            token += c[i];
                            i++;
                        }
                        OutputTxt.Text += "lexeme: " + token + " , token: number \n";
                        token = "";
                        if (i == c.Length) state = states.DONE;
                        else state = states.START;
                        break;

                    case states.IDENTIFIER:
                        while (isLetter(c[i]))
                        {
                           while(isLetter(c[i]) || isDigit(c[i]))
                            {
                                token += c[i];
                                i++;
                            }
                        }
                        for (int j = 0; j < ResWords.Length; j++)
                        {
                            if (ResWords[j] == token) ResFlag = true;
                        }
                        if (ResFlag)
                        {
                            switch(token)
                            {
                                case "int":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: int \n";
                                        break;
                                    }

                                case "float":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: float \n";
                                        break;
                                    }
                                case "string":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: string \n";
                                        break;
                                    }
                                case "read":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: read \n";
                                        break;
                                    }
                                case "write":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: write \n";
                                        break;
                                    }
                                case "repeat":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: repeat \n";
                                        break;
                                    }
                                case "until":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: until \n";
                                        break;
                                    }
                                case "if":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: if \n";
                                        break;
                                    }
                                case "elseif":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: elseif \n";
                                        break;
                                    }
                                case "else":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: else \n";
                                        break;
                                    }
                                case "then":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: then \n";
                                        break;
                                    }
                                case "return":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: return \n";
                                        break;
                                    }
                                case "endl":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: endl \n";
                                        break;
                                    }
                                case "end":
                                    {
                                        OutputTxt.Text += "lexeme: " + token + " , token: end \n";
                                        break;
                                    }
                            }
                        }
                        else
                        {   
                            if (c[i] == '(')
                            {
                                while (c[i] != ')')
                                {
                                    token += c[i];
                                    i++;
                                }
                                token += c[i];
                                i++;
                                OutputTxt.Text +="lexeme: " + token + " , token: function call \n";
                            }
                            else OutputTxt.Text += "lexeme: " + token + " , token: Identifier \n";
                        }
                        token = "";
                        ResFlag = false;
                        if (i == c.Length) state = states.DONE;
                        else state = states.START;
                        break;

                    /* case states.RESERVED:
                         while(isLetter(c[i]))
                         {
                             token += c[i];
                             i++;
                         }
                         if (isReserved(token))
                             OutputTxt.Text += token + " , reserved word \n";
                         token = "";
                         if (i == c.Length) state = states.DONE;
                         else state = states.START;
                         break; */
                                                                
                    case states.ASSIGN:
                        if (c[i] == ':') 
                        {
                            i += 2;
                            OutputTxt.Text += "lexeme: := , token: assign operator \n";
                            state = states.START;
                        }
                        else
                        {
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                        }
                        break;

                    case states.STRING:
                        if (state == states.STRING)
                        {
                            while (c[i] != '"')
                            {
                                token += c[i];
                                i++;
                            }
                            OutputTxt.Text += "lexeme: " + token + " , token: string \n";
                            token = "";
                            i++;
                            if (i == c.Length) state = states.DONE;
                            else state = states.START;
                        }
                        break;

                    case states.DONE:
                        break;

                }
            }
        }


        /* DO NOT DELETE THIS EMPTY METHOD OR THE PROGRAM WILL CRASH */
        private void RichTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        /* code for the clear button */
        private void Button2_Click(object sender, EventArgs e)
        {
            OutputTxt.Text = "";
            InputTxt.Text = "";
            state = states.START;
        }


    }
}
