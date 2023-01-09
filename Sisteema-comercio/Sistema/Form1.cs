using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sistema
{
    public partial class frm_home : Form
    {
        //Variaveis para o modulo caixa capturar os campos da esntidade produto.
        private int quantidade_produto;
        string nome_produto, valor;
        int codigo_produto;

        //Se estiver produto no estoque
        bool produto_carrinho;

        //Metodo Construtor da class
        public frm_home()
        {
            InitializeComponent();
        }

        //loadForm Carregar a pagina..
        private void frm_home_Load(object sender, EventArgs e)
        {
            produto_carrinho = false;
            atualizar();
        }

        //atualizar todos os DataGridView
        public void atualizar()
        {       
            Estoque estoque = new Estoque();
            dtg_estoque.DataSource = estoque.consultaEstoque();
            Estoque produto = new Estoque();
            dtg_produtos.DataSource = produto.consultaEstoque();
            Estoque caixaProduto = new Estoque();
            dtg_caixa_produto.DataSource = caixaProduto.consultaEstoque();   
        }

        //limpar todas as cores dos botões da sideBar
        public void cliarBtn()
        {
            btn_home.BackColor = Color.FromArgb(35, 40, 45);
            btn_caixa.BackColor = Color.FromArgb(35, 40, 45);
            btn_estoques.BackColor = Color.FromArgb(35, 40, 45);
            btn_sair.BackColor = Color.FromArgb(35, 40, 45);
            btn_cadastro_produto.BackColor = Color.FromArgb(35, 40, 45);
        }


        //Fechar todas as telas manter  so a tela Home a mostra
        public void closeTela()
        {
            flow_home.Visible = true;
            pl_caixa.Visible = false;
           pl_estoque.Visible = false;
            pl_cadastro_produtos.Visible = false;
        }

        //Fechar todas as telas
        public void closeTelaAll()
        {
            flow_home.Visible = false;
           pl_caixa.Visible = false;
            pl_estoque.Visible = false;
            pl_cadastro_produtos.Visible = false;
        }



        //BTN SIDE BAR
        private void btn_home_Click(object sender, EventArgs e)
        {
            cliarBtn();
            btn_home.BackColor = Color.Gray;
            closeTela();
        }

        //btn para tela caixa
        private void btn_caixa_Click(object sender, EventArgs e)
        {
            cliarBtn();
            btn_caixa.BackColor = Color.Gray;
            closeTelaAll();
            pl_caixa.Visible = true;
        }

        //btn para tela estoque
        private void btn_estoque_Click(object sender, EventArgs e)
        {
           cliarBtn();
           btn_estoques.BackColor = Color.Gray;
            closeTelaAll();
            pl_estoque.Visible = true;
            
        }

        //btn para tela sair
        private void btn_sair_Click(object sender, EventArgs e)
        {
            if (produto_carrinho == true)
            {
                if (MessageBox.Show("Tem item no carrinho, mesmo assim deseja sair?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Carrinho carrinhoDelete = new Carrinho();
                    carrinhoDelete.limparCarrinho();
                    Close();
                }
               
            }
            else
                Close();
           
        }
        
        //btn caixa 
        private void btn_home_caixa_Click(object sender, EventArgs e)
        {
          pl_caixa.Visible = true;
        }

        //btn estoque 
        private void btn_home_estoque_Click(object sender, EventArgs e)
        {
            cliarBtn();
            btn_estoque.BackColor = Color.Gray;
            pl_estoque.Visible = true;
        }

        //btn sair
        private void btn_home_sair_Click(object sender, EventArgs e)
        {

            if (produto_carrinho == true)
            {
                if (MessageBox.Show("Tem item no carrinho, mesmo assim deseja sair?", "Alerta", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Carrinho carrinhoDelete = new Carrinho();
                    carrinhoDelete.limparCarrinho();
                    Close();
                }      
            }
            else
                Close();

        }


        private void button1_Click(object sender, EventArgs e)
        {
            closeTela();
        }

       

        private void button3_Click(object sender, EventArgs e)
        {
            atualizar();
        }

        private void btn_salvar_Click(object sender, EventArgs e)
        {
          
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Estoque estoque = new Estoque();
            dtg_estoque.DataSource = estoque.consultaEstoque();
        }


        //ENTIDADE ESTOQUE

        //Consultas no estoque
        private void txt_estoque_pesquisa_TextChanged(object sender, EventArgs e)
        {
            //se for diferente de vazio
            if (txt_estoque_pesquisa.Text != "")
            {
                //Consulta por Nome
                if(rad_estoque_nome.Checked == true)
                {
                    Estoque estoque = new Estoque();
                    dtg_estoque.DataSource = estoque.consultaEstoqueNome(txt_estoque_pesquisa.Text);
                }
                //Consulta por Tipo
                if (rad_estoque_tipo.Checked == true)
                {
                    Estoque estoque = new Estoque();
                    dtg_estoque.DataSource = estoque.consultaEstoqueTipo(txt_estoque_pesquisa.Text);

                }
                //Consulta por marca
                if (rad_estoque_marca.Checked == true)
                {
                    Estoque estoque = new Estoque();
                    dtg_estoque.DataSource = estoque.consultaEstoqueMarca(txt_estoque_pesquisa.Text);
                }
                //Consulta por Nome caso as tres opições seja vazio
                if (rad_estoque_nome.Checked == false && rad_estoque_tipo.Checked == false && rad_estoque_marca.Checked == false)
                {
                    Estoque estoque = new Estoque();
                    dtg_estoque.DataSource = estoque.consultaEstoqueNome(txt_estoque_pesquisa.Text);
                }
            }
            //Atualizar
            else
            {
                atualizar();
            }
               
        }

        //Apagar um registro do estoque 
        private void dtg_estoque_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtg_estoque.Columns[e.ColumnIndex].Name == "btn_estoque_deletar")
            {
                if(MessageBox.Show("Deseja apagar este produto do seu estoque","Delete",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Estoque estoque = new Estoque();
                    estoque.deletarEstoqueId(Convert.ToInt32(dtg_estoque.Rows[e.RowIndex].Cells["ID_ESTOQUE"].Value.ToString()));
                    atualizar();
                }
              
            }
        }


        //ENTIDADE PRODUTO///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //btn da side bar para a tela cadastro de produtos
        private void btn_cadastro_produto_Click(object sender, EventArgs e)
        {
            cliarBtn();
             btn_cadastro_produto.BackColor = Color.Gray;
            closeTelaAll();
            pl_cadastro_produtos.Visible = true;
        }

        //btn editar de produtos
        private void dtg_produtos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtg_produtos.Columns[e.ColumnIndex].Name == "btn_produto_editar_")
            {
                txt_produto_codigo.Text = dtg_produtos.Rows[e.RowIndex].Cells["ID_PRODUTO_"].Value.ToString();
                txt_produto_nome_produto.Text = dtg_produtos.Rows[e.RowIndex].Cells["NOME_PRODUTO_"].Value.ToString();
                txt_produto_tipo.Text =  dtg_produtos.Rows[e.RowIndex].Cells["TIPO_PRODUTO_"].Value.ToString();
                txt_produto_marca.Text = dtg_produtos.Rows[e.RowIndex].Cells["MARCA_PRODUTO_"].Value.ToString();
                txt_produto_quantidade.Text = dtg_produtos.Rows[e.RowIndex].Cells["QUANTIDADE_PRODUTO_"].Value.ToString();
                txt_produto_valor.Text  = dtg_produtos.Rows[e.RowIndex].Cells["VALOR_PRODUTO_"].Value.ToString();
            }
        }

        //Btn para salvar caso nao tem um id vai registrar um novo
        private void btn_salvar_produto_Click(object sender, EventArgs e)
        {
            if (txt_produto_codigo.Text =="")
            {
                if (txt_produto_nome_produto.Text == "" || txt_produto_marca.Text == ""|| txt_produto_quantidade.Text == ""|| txt_produto_tipo.Text == ""|| txt_produto_valor.Text == "")
                {
                    MessageBox.Show("Digite em todos os campos", "Campos*", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    Estoque produto = new Estoque();
                    produto.gravar(txt_produto_nome_produto.Text, txt_produto_tipo.Text, txt_produto_marca.Text, txt_produto_quantidade.Text, txt_produto_valor.Text);
                    txt_produto_nome_produto.Clear();
                    txt_produto_tipo.Clear();
                    txt_produto_marca.Clear();
                    txt_produto_quantidade.Value = 0;
                    txt_produto_valor.Clear();
                    txt_produto_nome_produto.Focus();
                    txt_produto_codigo.Clear();
                    txt_produto_valor.Text = "0000";
                    atualizar();
                }
                
            }
            //Caso tenha um id
            else
            {
                if (txt_produto_nome_produto.Text == "" || txt_produto_marca.Text == "" || txt_produto_quantidade.Text == "" || txt_produto_tipo.Text == "" || txt_produto_valor.Text == "")
                {
                    MessageBox.Show("Não foi possivel salvar, Precisa deixar todos os campos preenchidos", "Campos*", MessageBoxButtons.OK, MessageBoxIcon.Question);
                }
                else
                {
                    Estoque produto = new Estoque();
                    produto.atualizar(Convert.ToInt32(txt_produto_codigo.Text),txt_produto_nome_produto.Text, txt_produto_tipo.Text, txt_produto_marca.Text, txt_produto_quantidade.Text, txt_produto_valor.Text);
                    txt_produto_nome_produto.Clear();
                    txt_produto_tipo.Clear();
                    txt_produto_marca.Clear();
                    txt_produto_quantidade.Value = 0;
                    txt_produto_valor.Clear();
                    txt_produto_nome_produto.Focus();
                    txt_produto_codigo.Clear();
                    txt_produto_valor.Text = "0000";
                    atualizar();
                }
            }
        }

        //Link para cancelar todas as açoes
        private void link_produto_cancelar_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            txt_produto_codigo.Clear();
            txt_produto_nome_produto.Clear();
            txt_produto_tipo.Clear();
            txt_produto_marca.Clear();
            txt_produto_quantidade.Value = 0;
            txt_produto_valor.Clear();
            txt_produto_nome_produto.Focus();
        }

        //Caso tenha mas de 3 numeros em preço
        private void chek_quantidade_casas_CheckedChanged(object sender, EventArgs e)
        {
            if (chek_quantidade_casas.Checked == true)
            {
                txt_produto_valor.Mask = "$ 000,00";
                txt_produto_valor.Text = "000000";
            }
            else
                txt_produto_valor.Mask = "$ 00,00";
            txt_produto_valor.Text = "0000";
        }


        //caixa produto
        private void dtg_caixa_produto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //uma ação para adicionar no carrinho pegando todos os campo do estoque e passando para o carrinho
            if (dtg_caixa_produto.Columns[e.ColumnIndex].Name == "Adicionar")
            {
                
                //Obitendo os valores do estoque atraves do datagridview
                nome_produto = dtg_caixa_produto.Rows[e.RowIndex].Cells["Nome_produto_caixa"].Value.ToString();
                valor = dtg_caixa_produto.Rows[e.RowIndex].Cells["Valor_caixa"].Value.ToString();
                codigo_produto = Convert.ToInt32(dtg_caixa_produto.Rows[e.RowIndex].Cells["Id_produto"].Value.ToString());
                quantidade_produto = Convert.ToInt32(dtg_caixa_produto.Rows[e.RowIndex].Cells["Quantidade_caixa"].Value.ToString());
                

                //Desabilitando componente
                dtg_caixa_produto.Enabled = false;
                txt_caixa_quantidade.Enabled = true;
                txt_caixa_quantidade.Focus();
                add_caixa.Enabled = true;
               
                
            }
        }

       
        //Removar do carrinho
        private void dtg_carrinho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dtg_carrinho.Columns[e.ColumnIndex].Name == "Remover_carrinho")
            {
                //estancia das classs
                Estoque estoque = new Estoque();
                Carrinho carrinho = new Carrinho();
                Carrinho carrinhoSelect = new Carrinho();


                //todas as  variaveis necessarias 
                int soma, valor_estoque_anterior, quantidade, id, id_carrinho;

                //obtendo os valores do DataGridView do Carrinho
                quantidade = Convert.ToInt32(dtg_carrinho.Rows[e.RowIndex].Cells["QUANTIDADE_CARRINHO"].Value.ToString());
                valor_estoque_anterior=  Convert.ToInt32(dtg_carrinho.Rows[e.RowIndex].Cells["Quantidade_estoque_anterior"].Value.ToString());
                id = Convert.ToInt32(dtg_carrinho.Rows[e.RowIndex].Cells["Codigo_produto_"].Value.ToString());
                id_carrinho = Convert.ToInt32(dtg_carrinho.Rows[e.RowIndex].Cells["ID_CARRINHO"].Value.ToString());

                //soma para devolver o produto para o estoque
                soma = valor_estoque_anterior + quantidade;

                //atualizar os datagridview
                estoque.updateEstoqueQuantidade(id, Convert.ToString(soma));
                carrinho.deletarPorID(id_carrinho);


                atualizar();
                dtg_carrinho.DataSource = carrinhoSelect.consultaCarrinho();

            }
        }

        //btn para adicionar no carrinho
        private void add_caixa_Click(object sender, EventArgs e)
        {
            //Soma para diminuir estoque
            if (txt_caixa_quantidade.Text == "")
            {
                MessageBox.Show("Digite a quantidade", "Estoque", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                if (quantidade_produto < Convert.ToInt32(txt_caixa_quantidade.Text) || quantidade_produto == 0)
                {
                       MessageBox.Show("Descupe mas a quantidade que deseja Não esta disponivel no estoque", "Estoque", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       txt_caixa_quantidade.Enabled = false;
                       add_caixa.Enabled = false;
                       txt_caixa_quantidade.Value = 0;
                       dtg_caixa_produto.Enabled = true;
                }
                else
                {
                    produto_carrinho = true;
                    int soma = quantidade_produto - Convert.ToInt32(txt_caixa_quantidade.Text);
                    Carrinho carrinhoSelect = new Carrinho();
                    Carrinho carrinhoGravar = new Carrinho();
                    carrinhoGravar.addNoCarrinho(nome_produto, txt_caixa_quantidade.Text, valor, codigo_produto, Convert.ToString(soma));
                    dtg_carrinho.DataSource = carrinhoSelect.consultaCarrinho();


                    Estoque estoque = new Estoque();
                    estoque.updateEstoqueQuantidade(codigo_produto,Convert.ToString(soma));
                    atualizar();


                    txt_caixa_quantidade.Enabled = false;
                    add_caixa.Enabled = false;
                    txt_caixa_quantidade.Value = 0;
                    dtg_caixa_produto.Enabled = true;
                }
            }
        }
    }
}
