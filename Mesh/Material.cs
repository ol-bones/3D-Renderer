using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace 3DRenderer
{
    struct Material
    {
        public Vector3 AmbientReflectivity, DiffuseReflectivity, SpecularReflectivity;
        public float Shininess, Opacity;
        public Matrix2 Spin;

        public void SetShininess(float s)
        {
            Shininess = s * 128;
        }

        public void SetOpacity(float o)
        {
            Opacity = o;
        }

        public void SetSpin(Matrix2 s)
        {
            Spin = s;
        }


        public void SetAmbientReflectivity(Vector3 ar)
        {
            AmbientReflectivity = ar;
        }

        public void SetDiffuseReflectivity(Vector3 dr)
        {
            DiffuseReflectivity = dr;
        }

        public void SetSpecularReflectivity(Vector3 sr)
        {
            SpecularReflectivity = sr;
        }

        public Material Default()
        {
            Material m = new Material();

            m.SetShininess(5.0f);
            m.SetOpacity(1.0f);

            m.SetAmbientReflectivity(new Vector3(1.0f, 1.0f, 1.0f));
            m.SetDiffuseReflectivity(new Vector3(1.0f, 1.0f, 1.0f));
            m.SetSpecularReflectivity(new Vector3(1.0f, 1.0f, 1.0f));
            m.SetSpin(Matrix2.CreateRotation(0));
            return m;
        }
    }
}
