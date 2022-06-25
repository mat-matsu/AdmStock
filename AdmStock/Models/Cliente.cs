using System.ComponentModel.DataAnnotations;

namespace AdmStock.Models
{
    public class Cliente
    {
        [Key]
        public int cliente_id { get; set; }
        [Required]
        public string cliente_nom { get; set; }
        [Required]
        public int cliente_dni { get; set; }
        [Required]
        public string cliente_dir { get; set; }
        [Required]
        public string cliente_tel { get; set; }

        List<Cliente> clientes = new List<Cliente>();

        Cliente buscarCliente(int id)
        {
            Cliente cliente = null;
            try
            {
                cliente = clientes.Find(c => c.cliente_id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
            return cliente;
        }
        Cliente buscarClientePorDni(int dni)
        {
            Cliente cliente = null;
            try
            {
                cliente = clientes.Find(c => c.cliente_dni == dni);
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return cliente;
        }
        bool agregarCliente(String nombre, int dni, String dir, String telefono)
        {
            Boolean pude = false;
            Cliente clienteAux;
            clienteAux = buscarCliente(dni);
            if (clienteAux == null)
            {
                Cliente cliente = null;
                cliente = new Cliente();
                int id;
                Cliente clienteAux2;
                id = clientes.Count();
                cliente.cliente_id = id + 1;
                clienteAux2 = buscarCliente(id);
                while (clienteAux2 != null)
                {
                    id++;
                    Cliente clienteAux3 = clienteAux2;
                    clienteAux2 = buscarCliente(id);
                    if (clienteAux2 == null)
                    {
                        cliente.cliente_id = clienteAux3.cliente_id + 1;
                    }
                }
                cliente.cliente_nom = nombre;
                cliente.cliente_dni = dni;
                cliente.cliente_dir = dir;
                cliente.cliente_tel = telefono;
                clientes.Add(cliente);
                pude = true;
            }
            return pude;

        }
        bool removerCliente(int id)
        {
            Boolean remover = false;
            Cliente cliente = clientes.Find(c => c.cliente_id == id);
            if (cliente != null)
            {
                clientes.Remove(cliente);
                remover = true;
            }
            return remover;

        }
        bool modificarCliente(int id)
        {
            Boolean modificar = false;
            Cliente cliente = clientes.Find(c => c.cliente_id == id);
            if (cliente != null)
            {
                Console.WriteLine("Nombre del cliente: ");
                String nombre = Console.ReadLine();
                Console.WriteLine("DNI del cliente: ");
                int dni = int.Parse(Console.ReadLine());
                Console.WriteLine("Direccion del cliente: ");
                String dir = Console.ReadLine();
                Console.WriteLine("Teléfono del cliente: ");
                String telefono = Console.ReadLine();
                cliente.cliente_nom = nombre;
                cliente.cliente_dni = dni;
                cliente.cliente_dir = dir;
                cliente.cliente_tel = telefono;
                modificar = true;
            }
            return modificar;
        }
        void listarClientes()
        {
            for (int i = 0; i < clientes.Count; i++)
            {
                Console.WriteLine("ID: " + clientes[i].cliente_id);
                Console.WriteLine("Nombre: " + clientes[i].cliente_nom);
                Console.WriteLine("DNI: " + clientes[i].cliente_dni);
                Console.WriteLine("Direccion: " + clientes[i].cliente_dir);
                Console.WriteLine("Teléfono: " + clientes[i].cliente_tel);
            }
        }
    }
}
