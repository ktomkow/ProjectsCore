using System;
using System.Diagnostics;
using System.Reflection;

namespace ProjectsCore.Reflections
{
    public static class CallerResolver
    {
        public static string ResolveName()
        {
            string name;
            Type declaringType;
            int skipFrames = 1;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                name = declaringType.Name;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return name;
        }

        public static string ResolveFullName()
        {
            string fullName;
            Type declaringType;
            int skipFrames = 1;
            do
            {
                MethodBase method = new StackFrame(skipFrames, false).GetMethod();
                declaringType = method.DeclaringType;
                if (declaringType == null)
                {
                    return method.Name;
                }
                skipFrames++;
                fullName = declaringType.FullName;
            }
            while (declaringType.Module.Name.Equals("mscorlib.dll", StringComparison.OrdinalIgnoreCase));

            return fullName;
        }
    }
}
