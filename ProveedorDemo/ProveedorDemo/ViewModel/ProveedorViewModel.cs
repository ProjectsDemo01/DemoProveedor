using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace ProveedorDemo.ViewModel
{
    public class ProveedorViewModel : BaseViewModel
    {
        #region Atributos
        private string rfc;
        private string razonsocial;
        private string domicilio;
        private string accountaddress;
        #endregion

        #region Propiedades
        public string RFC
        {
            get { return this.rfc; }//
            set { this.rfc = value; }//
        }

        public string RazonSocial
        {
            get { return this.razonsocial; }
            set { this.razonsocial = value; }
        }

        public string Domicilio
        {
            get { return this.domicilio; }
            set { this.domicilio = value; }
        }

        public string AccountAddress
        {
            get { return this.accountaddress; }
            set { this.accountaddress = value; }
        }
        #endregion

        #region Metodos
        public ICommand RegisterProveedor
        {
            get
            {
                return new RelayCommand(proveedor);
            }
        }

        public async void proveedor()
        {
            try
            {
                //var rfcPattern = "^[A-Z0-9]\\d{3,4}[0-9]\\d{6}[A-Z0-9]\\d{3}$  ";
                //if (!string.IsNullOrEmpty(RFC) && !(Regex.IsMatch(RFC, rfcPattern)))
                //{
                //    await Application.Current.MainPage.DisplayAlert(
                //        "Advertencia",//"Error",
                //        "Debe ingresar su RFC",//"Debe de ingresar el patron de su RFC",
                //        "Aceptar");
                //    return;
                //}
                if (string.IsNullOrEmpty(this.RFC) ||
                    string.IsNullOrEmpty(this.RazonSocial) ||
                    string.IsNullOrEmpty(this.Domicilio) ||
                    string.IsNullOrEmpty(this.AccountAddress))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Advertencia",//"Error",
                        "Debe ingresar todos los datos del Proveedor",//"Debe de ingresar los datos",
                        "Aceptar");
                    return;
                }


                //NethereumWeb3.RegistroDeProveedor();
                ToolsClass.RegistroDeProveedor(this.RFC, this.RazonSocial, this.Domicilio, this.AccountAddress);


                await Application.Current.MainPage.DisplayAlert(
                       "Aviso",
                       "Se guardó con exito en la BLOCKCHAIN NETHEREUM Y CON XAMARIN.!!!! nada de FLUTTER..!!!",
                       "Aceptar");
                return;
                //if (isSave)
                //{

                //}
                //else
                //{
                //    await Application.Current.MainPage.DisplayAlert(
                //       "UPSS",
                //       "Ocurrio un error al guardar la info.",
                //       "Ok");
                //    return;
                //}
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommand SearchProveedor
        {
            get
            {
                return new RelayCommand(searchproveedor);
            }
        }

        public async void searchproveedor()
        {
            try
            {
                await Application.Current.MainPage.Navigation.PushAsync(new BuscadorProveedorPage());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ICommand BuscarProveedor
        {
            get
            {
                return new RelayCommand(buscarproveedor);
            }
        }

        public async void buscarproveedor()
        {
            try
            {
                //var rfcPattern = "/^([A-Z][AEIOUX][A-Z]{2}\\d{2}(?:0[1-9]|1[0-2])(?:0[1-9]|[12]\\d|3[01])[HM](?:AS|B[CS]|C[CLMSH]|D[FG]|G[TR]|HG|JC|M[CNS]|N[ETL]|OC|PL|Q[TR]|S[PLR]|T[CSL]|VZ|YN|ZS)[B-DF-HJ-NP-TV-Z]{3}[A-Z\\d])(\\d)$/";
                if (string.IsNullOrEmpty(this.RFC))
                {
                    await Application.Current.MainPage.DisplayAlert(
                        "Advertencia",//"Error",
                        "Debe ingresar su RFC",//"Debe de ingresar el patron de su RFC",
                        "Aceptar");
                    return;
                }

                bool existe = ToolsClass.BuscarPorRFC(this.RFC);
                if (existe)
                {
                    await Application.Current.MainPage.DisplayAlert(
                       "Aviso",
                       "El RFC se encuentra registrado en la BLOCKCHAIN..!!!",
                       "Aceptar");
                    return;
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert(
                       "Error FATAL",
                       "El RFC no se encuentra registrado en la BLOCKCHAIN..!!!",
                       "Aceptar");
                    return;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public static bool AddProveedorWeb3(string _RFC, string _razonSocial, string _domicilio, string accountAddress)
        //{
        //    string url = "HTTP://localhost:7545";
        //    string address = "0xbFd2C06be67b9Cb50b2714EecBe2531544f53FdC";
        //    string ABI = @"[{'constant':true,'inputs':[{'name':'_RFC','type':'string'}],'name':'RequestProveedor','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_RFC','type':'string'},{'name':'_razonSocial','type':'string'},{'name':'_domicilio','type':'string'}],'name':'AddProveedor','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'}]";
        //    Web3 web3 = new Web3(url);
        //    Contract ProveedorContract = web3.Eth.GetContract(ABI, address);

        //    //string accountAddress = "aqui va el address de una cuenta";

        //    try
        //    {
        //        HexBigInteger gas = new HexBigInteger(new BigInteger(4000000));
        //        HexBigInteger value = new HexBigInteger(new BigInteger(0));
        //        Task<string> addProveedorFunction = ProveedorContract.GetFunction("AddProveedor").SendTransactionAsync(accountAddress, gas, value, _RFC, _razonSocial, _domicilio);
        //        addProveedorFunction.Wait();
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error:", ex.Message);
        //        throw ex;
        //    }
        //}

        //public static bool GetRFCWeb3(string RFC)
        //{
        //    string url = "HTTP://localhost:7545";
        //    string address = "0xbFd2C06be67b9Cb50b2714EecBe2531544f53FdC";
        //    string ABI = @"[{'constant':true,'inputs':[{'name':'_RFC','type':'string'}],'name':'RequestProveedor','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_RFC','type':'string'},{'name':'_razonSocial','type':'string'},{'name':'_domicilio','type':'string'}],'name':'AddProveedor','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'}]";
        //    Web3 web3 = new Web3(url);
        //    Contract ProveedorContract = web3.Eth.GetContract(ABI, address);

        //    //string accountAddress = "aqui va el address de una cuenta";

        //    try
        //    {
        //        bool existe = false;
        //        HexBigInteger gas = new HexBigInteger(new BigInteger(4000000));
        //        HexBigInteger value = new HexBigInteger(new BigInteger(0));
        //        Task<Boolean> getRFCFunction = ProveedorContract.GetFunction("RequestProveedor").CallAsync<Boolean>(RFC);
        //        getRFCFunction.Wait();
        //        existe = getRFCFunction.Result;
        //        //Console.WriteLine(temp);
        //        return existe;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error:", ex.Message);
        //        throw ex;
        //    }
        //}
        #endregion
    }
}
