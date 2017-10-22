using System;
using System.IO;

namespace sistema_vendas
{
    class Program
    {
        static void Main(string[] args)
        {
            int opcao = 0;

            while (opcao != 9)
            {
                Console.WriteLine("----------------------------------------");
                Console.WriteLine("------------SISTEMA DE VENDAS-----------");
                Console.WriteLine("----------------------------------------");
                Console.WriteLine();
                Console.WriteLine("Digite o número da opção que deseja fazer:");
                Console.WriteLine("1 - Cadastro de clientes");
                Console.WriteLine("2 - Cadastro de produtos");
                Console.WriteLine("3 - Realizar vendas");
                Console.WriteLine("4 - Extrato de cliente");
                Console.WriteLine("9 - Sair");

                opcao = Int32.Parse(Console.ReadLine());


                switch (opcao)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("---------------------------");
                        Console.WriteLine("-----Cadastrar cliente-----");
                        Console.WriteLine("---------------------------");
                        Console.Clear();
                        String cpf_cnpj = "";
                        Console.Write("Nome completo: ");
                        String nome = Console.ReadLine();
                        Console.Write("E-mail: ");
                        String email = Console.ReadLine();
                        Console.Write("Digite 1 para PF ou 2 para PJ: ");
                        int tipo_pf_pj = Int32.Parse(Console.ReadLine());
                        if (tipo_pf_pj == 1)
                        {
                            Console.Write("CPF (apenas números):");
                            cpf_cnpj = Console.ReadLine();
                            while (validaCpf(cpf_cnpj) == false)
                            {
                                Console.Write("Digite um CPF valido (apenas números):");
                                cpf_cnpj = Console.ReadLine();
                            }
                            Console.WriteLine();
                            Console.WriteLine("Cliente cadastrado com sucesso");
                        }
                        if (tipo_pf_pj == 2)
                        {
                            Console.Write("CNPJ (apenas números):");
                            cpf_cnpj = Console.ReadLine();
                            while (validaCnpj(cpf_cnpj) == false)
                            {
                                Console.Write("Digite um CNPJ valido (apenas números):");
                                cpf_cnpj = Console.ReadLine();
                            }
                            Console.WriteLine();
                            Console.WriteLine("Cliente cadastrado com sucesso");
                        }

                        gravaClientes(nome, email, tipo_pf_pj, cpf_cnpj);

                        break;

                    case 2:
                        Console.Clear();
                        Console.WriteLine("---------------------------");
                        Console.WriteLine("-----Cadastrar produto-----");
                        Console.WriteLine("---------------------------");
                        Console.Write("Código do produto: ");
                        int cod_produto = Int16.Parse(Console.ReadLine());
                        Console.Write("Nome do produto: ");
                        String nome_produto = Console.ReadLine();
                        Console.Write("Descrição do produto: ");
                        String desc_produto = Console.ReadLine();
                        Console.Write("Preço do produto: ");
                        double preco = Double.Parse(Console.ReadLine());

                        gravaProdutos(cod_produto, nome_produto, desc_produto, preco);
                        
                        Console.WriteLine();
                        Console.WriteLine("Produto cadastrado com sucesso");
                        break;

                    case 3:
                        Console.Clear();
                        Console.WriteLine("------------------------");
                        Console.WriteLine("-----Realizar venda-----");
                        Console.WriteLine("------------------------");
                        Console.WriteLine();
                        Console.Write("Digite o CPF/CNPJ: ");
                        cpf_cnpj = Console.ReadLine();

                        String dados = consultaDadosCliente(cpf_cnpj);
                        // Console.WriteLine(dados);
                        String[] dadosCliente;

                        if ((dados != ""))
                        {
                            dadosCliente = dados.Split(';');
                            Console.WriteLine("Nome completo: " + dadosCliente[0]);
                            Console.WriteLine("E-mail: " + dadosCliente[1]);
                            Console.WriteLine("Tipo de documento: " + dadosCliente[2]);
                            Console.WriteLine("Documento: " + dadosCliente[3]);
                            Console.WriteLine("Data de cadastro: " + dadosCliente[4]);
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Cliente não cadastrado");
                            break;
                        }

                        Console.Write("Digito o código do produto: ");
                        int codProduto = Int16.Parse(Console.ReadLine());

                        if (consultaIdProduto(codProduto))
                        {
                            gravaVendas(cpf_cnpj, codProduto);
                            Console.WriteLine();
                            Console.WriteLine("Venda registrada com sucesso");
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.WriteLine("Produto não cadastrado");
                        }

                        break;

                    case 4:
                        Console.Clear();
                        Console.WriteLine("----------------------------");
                        Console.WriteLine("-----Extrato do cliente-----");
                        Console.WriteLine("----------------------------");
                        Console.WriteLine();

                        Console.WriteLine("Digite 1 para PF e 2 para PJ");
                        tipo_pf_pj = Int16.Parse(Console.ReadLine());
                        switch (tipo_pf_pj)
                        {
                            case 1:
                                Console.Write("Digite o CPF: ");
                                cpf_cnpj = Console.ReadLine();
                                if (validaCpf(cpf_cnpj))
                                {
                                    extratoCliente(cpf_cnpj);
                                }
                                else
                                {
                                    Console.WriteLine("CPF inválido");
                                }
                                break;
                            case 2:
                                Console.Write("Digite o CNPJ: ");
                                cpf_cnpj = Console.ReadLine();
                                if (validaCnpj(cpf_cnpj))
                                {
                                    extratoCliente(cpf_cnpj);
                                }
                                else
                                {
                                    Console.WriteLine("CNPJ inválido");
                                }
                                break;
                            default:
                                Console.WriteLine("Opção inválida");
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }
        static void cadastroClientes(String nome, String email, int tipo_pf_pj, String cpf_cnpj)
        {
        }

        /// <summary>
        /// Essa função valida o CPF digitado pelo usuário
        /// </summary>
        /// <param name="cpfEntrada">O CPF deve ter apenas números, sem pontos e ífen.</param>
        /// <returns>O retorno é booleano (true / false)</returns>
        static bool validaCpf(string cpfEntrada)
        {
            bool retorno = true;
            string cpfCalculado = cpfEntrada.Substring(0, 9);
            int[] multiplicador1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] multiplicador2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int soma = 0;
            for (int i = 0; i < cpfCalculado.Length; i++)
            {
                soma += (Int32.Parse(cpfCalculado.Substring(i, 1)) * multiplicador1[i]);
            }
            if (soma % 11 < 2)
            {
                cpfCalculado += 0;
            }
            else
            {
                cpfCalculado += (11 - (soma % 11));
            }

            soma = 0;
            for (int i = 0; i < cpfCalculado.Length; i++)
            {
                soma += (Int32.Parse(cpfCalculado.Substring(i, 1)) * multiplicador2[i]);
            }

            if (soma % 11 < 2)
            {
                cpfCalculado += 0;
            }
            else
            {
                cpfCalculado += (11 - (soma % 11));
            }
            if (cpfCalculado != cpfEntrada)
            {
                retorno = false;
                return retorno;
            }
            return retorno;
        }

        static bool validaCnpj(string cnpj)
        {
            int[] v1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] v2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

            bool retorno = true;

            string cnpjcalculo = "";
            int resultado = 0;
            int resto = 0;

            cnpjcalculo = cnpj.Substring(0, 12);

            for (int i = 0; i < cnpjcalculo.Length; i++)
            {
                resultado += Int32.Parse(cnpjcalculo[i].ToString()) * v1[i];
            }
            resto = resultado % 11;

            if (resto < 2)
            {
                cnpjcalculo += "0";
            }
            else
            {
                cnpjcalculo += (11 - resto).ToString();
            }

            resultado = 0;

            for (int i = 0; i < cnpjcalculo.Length; i++)
            {
                resultado += Int32.Parse(cnpjcalculo[i].ToString()) * v2[i];
            }
            resto = resultado % 11;

            if (resto < 2)
            {
                cnpjcalculo += "0";
            }
            else
            {
                cnpjcalculo += (11 - resto).ToString();
            }

            if (cnpjcalculo != cnpj)
            {
                retorno = false;
            }
            return retorno;
        }

        /// <summary>
        /// Função para gravar os clientes no arquivo "_Clientes.txt";
        /// </summary>
        /// <param name="nome">Parametro do tipo String que recebe o nome completo do Cliente</param>
        /// <param name="email">Parametro do tipo String que recebe o e-mail do Cliente</param>
        /// <param name="tipo_pf_pj">Parametro do tipo int que recebe o tipo de documento do Cliente</param>
        /// <param name="cpf_cnpj">Parametro do tipo String que recebe o CPF/CNPJ do Cliente</param>
        static void gravaClientes(String nome, string email, int tipo_pf_pj, String cpf_cnpj)
        {
            FileInfo fiClientes = new FileInfo("_Clientes.csv");
            StreamWriter swClientes = new StreamWriter("_Clientes.csv", true);
            if (fiClientes.Length > 0)
            {
                swClientes.WriteLine(nome + ";" + email + ";" + tipo_pf_pj + ";" + cpf_cnpj + ";" + DateTime.Now.ToShortDateString());
            }
            else
            {
                swClientes.WriteLine("nome_completo;email;tipo_pf_pj;cpf_cnpj;data_cadastro");
                swClientes.WriteLine(nome + ";" + email + ";" + tipo_pf_pj + ";" + cpf_cnpj + ";" + DateTime.Now.ToShortDateString());
            }
            swClientes.Close();
        }

        /// <summary>
        /// Função para gravar os clientes no arquivo "_Produtos.txt";
        /// </summary>
        /// <param name="cod_produto">Parametro do tipo int que recebe o código do produto</param>
        /// <param name="nome_produto">Paratro do tipo String que recebe o nome do produto</param>
        /// <param name="desc_produto">Parametro do tipo String que recebe a descrição do produto</param>
        /// <param name="preco">Parametro do tipo double que recebe o preço do produto</param>
        static void gravaProdutos(int cod_produto, string nome_produto, string desc_produto, double preco)
        {
            FileInfo fiProdutos = new FileInfo("_Produtos.csv");
            StreamWriter swProdutos = new StreamWriter("_Produtos.csv", true);
            if (fiProdutos.Length > 0)
            {
                swProdutos.WriteLine(cod_produto + ";" + nome_produto + ";" + desc_produto + ";" + preco + ";" + DateTime.Now.ToShortDateString());
            }
            else
            {
                swProdutos.WriteLine("cod_produto;nome_produto;desc_produto;preco;data_cadastro");
                swProdutos.WriteLine(cod_produto + ";" + nome_produto + ";" + desc_produto + ";" + preco + ";" + DateTime.Now.ToShortDateString());
            }
            swProdutos.Close();
        }

        static void gravaVendas(String cpf_cnpj, int cod_produto)
        {
            FileInfo fiVendas = new FileInfo("_Vendas.csv");
            StreamWriter swVendas = new StreamWriter("_Vendas.csv", true);
            if (fiVendas.Length > 0)
            {
                swVendas.WriteLine(cpf_cnpj + ";" + cod_produto + ";" + DateTime.Now.ToShortDateString());
            }
            else
            {
                swVendas.WriteLine("cpf_cnpj;cod_produto;data_venda");
                swVendas.WriteLine(cpf_cnpj + ";" + cod_produto + ";" + DateTime.Now.ToShortDateString());
            }
            swVendas.Close();
        }


        /// <summary>
        /// Função para consultar o CPF/CNPJ no arquivo de "_Clientes.txt" e validar se o cliente já está cadastrado
        /// </summary>
        /// <param name="cpf_cnpj">Parametro do tipo String que recebe o CPF/CNPJ que será consultado</param>
        /// <returns>Retorno do tipo Boolean, caso o retorno seja TRUE, o cliente já está cadastrado, se FALSE, o cliente ainda não está cadastrado.</returns>
        static bool consultaCpfCnpj(String cpf_cnpj)
        {
            bool retorno = false;
            StreamReader srClientes = new StreamReader("_Clientes.csv");
            String txt = "";

            while ((txt = srClientes.ReadLine()) != null)
            {
                if (txt.Contains(cpf_cnpj))
                {
                    retorno = true;
                }
            }
            srClientes.Close();
            return retorno;
        }

        /// <summary>
        /// Função para consultado dos dados do cliente, caso ele já esteja cadastrado.
        /// </summary>
        /// <param name="cpf_cnpj">Parametro do tipo String que recebe o CPF/CNPJ do cliente</param>
        /// <returns>Retorna os dados do cliente, caso ele exista.</returns>
        static String consultaDadosCliente(String cpf_cnpj)
        {
            StreamReader srClientes = new StreamReader("_Clientes.csv");
            String dadosCliente = "";
            String txt = "";

            // Console.WriteLine(consultaCpfCnpj(cpf_cnpj));
            if (consultaCpfCnpj(cpf_cnpj))
            {
                while ((txt = srClientes.ReadLine()) != null)
                {
                    if (txt.Contains(cpf_cnpj))
                    {
                        dadosCliente = txt;
                    }
                }
            }
            srClientes.Close();
            return dadosCliente;
        }

        /// <summary>
        /// Função para consultar o CPF/CNPJ no arquivo de "_Clientes.txt" e validar se o cliente já está cadastrado
        /// </summary>
        /// <param name="cpf_cnpj">Parametro do tipo String que recebe o CPF/CNPJ que será consultado</param>
        /// <returns>Retorno do tipo Boolean, caso o retorno seja TRUE, o cliente já está cadastrado, se FALSE, o cliente ainda não está cadastrado.</returns>
        static bool consultaIdProduto(int cod_produto)
        {
            bool retorno = false;
            StreamReader srProdutos = new StreamReader("_Produtos.csv");
            String txt = "";

            while ((txt = srProdutos.ReadLine()) != null)
            {
                if (txt.Contains(cod_produto.ToString()))
                {
                    retorno = true;
                }
            }
            srProdutos.Close();
            return retorno;
        }

        static void extratoCliente(String cpf_cnpj)
        {
            StreamReader srVendas = new StreamReader("_Vendas.csv");
            String txt = "";

            Console.WriteLine("\nExtrato do cliente:");
            Console.WriteLine("cpf_cnpj\tcod_produto\tdata_venda");
            while ((txt = srVendas.ReadLine()) != null)
            {
                if (txt.Contains(cpf_cnpj))
                {
                    Console.WriteLine(txt.Replace(";", "\t"));
                }
            }
        }
    }
}
