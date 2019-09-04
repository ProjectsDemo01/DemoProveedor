using Nethereum.Contracts;
using Nethereum.Hex.HexTypes;
using Nethereum.Web3;
using System;
using System.Numerics;
using System.Threading.Tasks;

namespace APIFramework
{
    public class ConectClass
    {
        public static string RegistroDeProveedor(string _RFC, string _razonSocial, string _domicilio) //SC
        {
            string url = "HTTP://localhost:7545";
            string address = "0x67cF7C9e6f529599bd7dd613BFA914B313c5Eb77";
            var rfc = _RFC;
            var razon = _razonSocial;
            var domicilio = _domicilio;

            string ABI = @"[{'constant':true,'inputs':[{'name':'_RFC','type':'string'}],'name':'RequestProveedor','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_RFC','type':'string'},{'name':'_razonSocial','type':'string'},{'name':'_domicilio','type':'string'}],'name':'AddProveedor','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'}]";
            Web3 web3 = new Web3(url);
            Contract ProveedorContract = web3.Eth.GetContract(ABI, address);
            string accountAddress = "0x1574D007c41deF6288A4Be6f270df67D473a6c41";

            //_RFC = Console.ReadLine();
            //_razonSocial = Console.ReadLine();
            //_domicilio = Console.ReadLine();
            
            try
            {
                HexBigInteger gas = new HexBigInteger(new BigInteger(4000000));
                HexBigInteger value = new HexBigInteger(new BigInteger(0));
                var addProveedorFunction = ProveedorContract.GetFunction("AddProveedor").SendTransactionAsync(accountAddress, gas, value, _RFC, _razonSocial, _domicilio);
                addProveedorFunction.Wait();
                var temp = addProveedorFunction.Result;

                Console.WriteLine(temp);
                Console.WriteLine("Registro Guardado!");
                return temp;

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
            string address = "0x67cF7C9e6f529599bd7dd613BFA914B313c5Eb77";
            string ABI = @"[{'constant':true,'inputs':[{'name':'_RFC','type':'string'}],'name':'RequestProveedor','outputs':[{'name':'','type':'bool'}],'payable':false,'stateMutability':'view','type':'function'},{'constant':false,'inputs':[{'name':'_RFC','type':'string'},{'name':'_razonSocial','type':'string'},{'name':'_domicilio','type':'string'}],'name':'AddProveedor','outputs':[],'payable':false,'stateMutability':'nonpayable','type':'function'}]";
            Web3 web3 = new Web3(url);
            Contract ProveedorContract = web3.Eth.GetContract(ABI, address);
            string accountAddress = "0x1574D007c41deF6288A4Be6f270df67D473a6c41";

            //string accountAddress = "aqui va el address de una cuenta";

            try
            {
                bool existe = false;
                HexBigInteger gas = new HexBigInteger(new BigInteger(4000000));
                HexBigInteger value = new HexBigInteger(new BigInteger(0));
                Task<Boolean> getRFCFunction = ProveedorContract.GetFunction("RequestProveedor").CallAsync<Boolean>(accountAddress, gas, value, _RFC);
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
