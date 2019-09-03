using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace API
{
    public class ClassNethereuWeb3
    {
        public static void RegistroDeProveedor(string _RFC, string _RazonSocial, string _Domicilio, string accountAddress_)
        {
            string url = "HTTP://localhost:7545";
            string address = "0xdDa4E4DfC0C6F55473Fe6082fDE76CB269fa5eE6";
            string ABI = @"[{'constant':true,'inputs':[{'name':'_RFC','type':'string'}],'name':'RequestProveedor','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_RFC','type':'string'},{'name':'_razonSocial','type':'string'},{'name':'_domicilio','type':'string'}],'name':'AddProveedor','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'}]";
            Web3 web3 = new Web3(url);
            Contract ProveedorContract = web3.Eth.GetContract(ABI, address);
            string accountAddress = "0x5013881a0f5a174510226Cd0D1d505a2Cc807dD8";

            try
            {
                HexBigInteger gas = new HexBigInteger(new BigInteger(4000000));
                HexBigInteger value = new HexBigInteger(new BigInteger(0));
                Task<string> addProveedorFunction = ProveedorContract.GetFunction("AddProveedor").SendTransactionAsync(accountAddress, gas, value, _RFC, _RazonSocial, _Domicilio);
                addProveedorFunction.Wait();
                //var temp = addProveedorFunction.Result;

                //Console.WriteLine(temp);
                //Console.WriteLine("Registro Guardado!");

                //return temp;

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
                throw ex;
            }
        }

        public static bool BuscarPorRFC(string _RFC)
        {
            string url = "HTTP://localhost:7545";
            string address = "0xdDa4E4DfC0C6F55473Fe6082fDE76CB269fa5eE6";
            string ABI = @"[{'constant':true,'inputs':[{'name':'_RFC','type':'string'}],'name':'RequestProveedor','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_RFC','type':'string'},{'name':'_razonSocial','type':'string'},{'name':'_domicilio','type':'string'}],'name':'AddProveedor','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'}]";
            Web3 web3 = new Web3(url);
            Contract ProveedorContract = web3.Eth.GetContract(ABI, address);

            //string accountAddress = "aqui va el address de una cuenta";

            try
            {
                bool existe = false;
                HexBigInteger gas = new HexBigInteger(new BigInteger(4000000));
                HexBigInteger value = new HexBigInteger(new BigInteger(0));
                Task<Boolean> getRFCFunction = ProveedorContract.GetFunction("RequestProveedor").CallAsync<Boolean>(_RFC);
                getRFCFunction.Wait();
                existe = getRFCFunction.Result;
                Console.WriteLine(existe);
                Console.WriteLine("El Proveedor si existe!");
                //Console.WriteLine(temp);
                return existe;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:", ex.Message);
                throw ex;
            }
        }
    }
}
