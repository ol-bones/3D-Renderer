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
    /// <summary>
        ///  Scene object for adding objects to the world
        ///  The ACW only requires one 'scene' and it
        ///  isn't translated or anything so there was no need
        ///  to expand it any further, so it's a bit redundant but still serves to
        ///  hold and manage Entities and Meshes in a convenient way.
    /// </summary>
    /// 
    class Scene
    {
        public List<Entity> Entities, EntitiesToRemove;
        public List<Mesh> Meshes;

        public int num_DirLights = 0;
        public int num_PosLights = 0;
        public int num_Ambientights = 0;

        public Vector3 Gravity;

        public Scene()
        {
            Entities = new List<Entity>();
            EntitiesToRemove = new List<Entity>();
            Meshes = new List<Mesh>();
        }

        public void Init()
        {
            this.Gravity = new Vector3(0.0f, -0.981f, 0.0f);
            //this.Gravity = new Vector3(0.0f, 0.0f, 0.0f);
        }

        public void Add(Entity e)
        {
            Entities.Add(e);
            e.ID = Entities.Count;
            if (e.GetType().IsSubclassOf(typeof(PhysicalEntity)))
            {
                PhysicalEntity physEnt = e as PhysicalEntity;
                Meshes.Add(physEnt.Mesh);
            }
            e.Init(this);
        }

        public void Remove(Entity e)
        {
            foreach (Entity Ent in Entities)
            {
                if (Ent == e)
                {
                    EntitiesToRemove.Add(Ent);

                    break;
                }
            }
        }

        public List<Entity> getEntities()
        {
            return this.Entities;
        }

        public Entity findEntityByID(int i)
        {
            foreach (Entity e in Entities)
            {
                if (e.ID == i) { return e; }
            }
            return null;
        }

        public void OnKeyUp(string k)
        {
            foreach (Entity e in Entities)
            {
                e.OnKeyUp(k);
            }
        }
        public void OnKeyDown(string k)
        {
            foreach (Entity e in Entities)
            {
                e.OnKeyDown(k);
            }
        }

        public void Delete()
        {
            foreach (Entity e in Entities)
            {
                e.Delete();
            }
            Entities.Clear();
            EntitiesToRemove.Clear();
            Meshes.Clear();
        }

        public void Update(float gameTime)
        {
            if (EntitiesToRemove.Count > 0)
            {
                foreach (Entity Ent in EntitiesToRemove)
                {
                    Entities.Remove(Ent);
                    Ent.Delete();
                }
                EntitiesToRemove.Clear();
            }

            foreach (Entity Ent in Entities)
            {
                Ent.Update(gameTime);
                if (!Ent.GetType().IsSubclassOf(typeof(PhysicalEntity)))
                {
                    continue;
                } 
                PhysicalEntity PhysEnt = Ent as PhysicalEntity;

                foreach (Entity pEnt in Entities)
                {
                    if (pEnt == Ent || !pEnt.GetType().IsSubclassOf(typeof(PhysicalEntity))) { continue; }
                    PhysicalEntity PhysEnt2 = pEnt as PhysicalEntity;

                    PhysEnt.DetectCollision(PhysEnt2);
                }
            }
        }

        public void Draw()
        {
            foreach (Entity e in Entities)
            {
                if (e.GetType().IsSubclassOf(typeof(PhysicalEntity)))
                {
                    (e as PhysicalEntity).Draw();
                }
            }
        }
    }
}
