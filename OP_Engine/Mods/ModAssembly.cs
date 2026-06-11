using System.Reflection;

namespace OP_Engine.Mods
{
    public class ModAssembly(string? name, string file_path)
    {
        private readonly Guid ID = Guid.NewGuid();
        public string? Name = name;
        public string? AssemblyPath = file_path;
        public Assembly? Assembly = Assembly.LoadFrom(file_path);
    }
}
