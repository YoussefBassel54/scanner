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
        string[] ResWords = { "int", "float", "string", "read", "write", "repeat", "until", "if", "elseif", "else", "then", "return", "endl" };


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

        private void RichTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

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
                        else if (c[i] == '/')
                        {
                            token += c[i];
                            i++;
                            if (c[i] == '*')
                                state = states.COMMENT;
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
                                    OutputTxt.Text += c[i] + " , semi colon\n";
                                    token = "";
                                    break;
                                default:
                                    OutputTxt.Text += c[i] + " , symbol\n";
                                    break;
                            }
                            i++;
                            if (i == c.Length)
                                state = states.DONE;
                            else state = states.START;
                        }
                        else if(isArithmatic(c[i]))
                        {
                            OutputTxt.Text += c[i] + " , arithmatic operator \n";
                            i++;
                        }
                        else if(isCondition(c[i]))
                        {
                            OutputTxt.Text += c[i] + " , condition operator \n";
                            i++;
                        }
                        else if(c[i] == '&' && c[i+1] == '&')
                        {
                            OutputTxt.Text += "&& , boolean operator \n";
                            i += 2;
                        }
                        else if(c[i]=='|' && c[i+1] =='|')
                        {
                            OutputTxt.Text += "|| , boolean operator \n";
                            i += 2;
                        }
                        else if(c[i] == '<' && c[i+1] == '>')
                        {
                            OutputTxt.Text += c[i] + c[i + 1] + " , condition operator \n";
                            i += 2;
                        }
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
                                OutputTxt.Text += token + " , comment \n";
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
                        OutputTxt.Text += token += " , number \n";
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
                        if (ResFlag) OutputTxt.Text += token + " , reserved word \n";
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
                                OutputTxt.Text += token + " , function call \n";
                            }
                            else OutputTxt.Text += token + " , Identifier \n";
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
                            OutputTxt.Text += " := , assign operator \n";
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
                            OutputTxt.Text += token + " , string \n";
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



        private void RichTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void Button2_Click(object sender, EventArgs e)
        {
            OutputTxt.Text = "";
            InputTxt.Text = "";
            state = states.START;
        }


    }
}
