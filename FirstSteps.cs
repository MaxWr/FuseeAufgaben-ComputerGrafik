using System;
using System.Collections.Generic;
using System.Linq;
using Fusee.Base.Common;
using Fusee.Base.Core;
using Fusee.Engine.Common;
using Fusee.Engine.Core;
using Fusee.Math.Core;
using Fusee.Serialization;
using Fusee.Xene;
using static System.Math;
using static Fusee.Engine.Core.Input;
using static Fusee.Engine.Core.Time;

namespace Fusee.Tutorial.Core
{
    public class FirstSteps : RenderCanvas
    {
        private TransformComponent _cubeTransform;
        private TransformComponent _cube1Transform;
        private TransformComponent _cube2Transform;
        private TransformComponent _cube3Transform;
        private TransformComponent _cube4Transform;
        private float _camAngle = 0;
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        // Init is called on startup. 
        public override void Init()
        {
  // Set the clear color for the backbuffer to white (100% intentsity in all color channels R, G, B, A).
      RC.ClearColor = new float4(0.7f, 1, 0.5f, 1);

      // Create a scene with a cube
      // The three components: one XForm, one Material and the Mesh
      _cubeTransform = new TransformComponent {Scale = new float3(1, 1, 1), Translation = new float3(2, 2, 2)};
      var cubeMaterial = new MaterialComponent
      {
          Diffuse = new MatChannelContainer {Color = new float3(0.7f, 0.3f, 0)},
          Specular = new SpecularChannelContainer {Color = float3.One, Shininess = 4}
      };
      var cubeMesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));
//----------------------------
      _cube1Transform = new TransformComponent {Scale = new float3(0, 0, 0), Translation = new float3(1, 2, 3)};
      var cube1Material = new MaterialComponent
      {
          Diffuse = new MatChannelContainer {Color = new float3(0f, 1f, 0)},
          Specular = new SpecularChannelContainer {Color = float3.One, Shininess = 4}
      };
      var cube1Mesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));
//-----------------------------
      _cube2Transform = new TransformComponent {Scale = new float3(0.5f, 2, 5), Translation = new float3(3, 2, 1)};
      var cube2Material = new MaterialComponent
      {
          Diffuse = new MatChannelContainer {Color = new float3(0.3f, 0.7f, 0)},
          Specular = new SpecularChannelContainer {Color = float3.One, Shininess = 4}
      };
      var cube2Mesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));
//-----------------------------
      _cube3Transform = new TransformComponent {Scale = new float3(1, 2, 3), Translation = new float3(4, 1, 5)};
      var cube3Material = new MaterialComponent
      {
          Diffuse = new MatChannelContainer {Color = new float3(0.2f, 0.3f, 0.2f)},
          Specular = new SpecularChannelContainer {Color = float3.One, Shininess = 4}
      };
      var cube3Mesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));
//-----------------------------
      _cube4Transform = new TransformComponent {Scale = new float3(1, 1, 1), Translation = new float3(2, 4, 2)};
      var cube4Material = new MaterialComponent
      {
          Diffuse = new MatChannelContainer {Color = new float3(0f, 0f, 0.5f)},
          Specular = new SpecularChannelContainer {Color = float3.One, Shininess = 4}
      };
      var cube4Mesh = SimpleMeshes.CreateCuboid(new float3(10, 10, 10));



      // Assemble the cube node containing the three components
      var cubeNode = new SceneNodeContainer();
      cubeNode.Components = new List<SceneComponentContainer>();
      cubeNode.Components.Add(_cubeTransform);
      cubeNode.Components.Add(cubeMaterial);
      cubeNode.Components.Add(cubeMesh);

      var cube1Node = new SceneNodeContainer();
      cube1Node.Components = new List<SceneComponentContainer>();
      cube1Node.Components.Add(_cube1Transform);
      cube1Node.Components.Add(cube1Material);
      cube1Node.Components.Add(cube1Mesh);

      var cube2Node = new SceneNodeContainer();
      cube2Node.Components = new List<SceneComponentContainer>();
      cube2Node.Components.Add(_cube2Transform);
      cube2Node.Components.Add(cube2Material);
      cube2Node.Components.Add(cube2Mesh);

      var cube3Node = new SceneNodeContainer();
      cube3Node.Components = new List<SceneComponentContainer>();
      cube3Node.Components.Add(_cube3Transform);
      cube3Node.Components.Add(cube3Material);
      cube3Node.Components.Add(cube3Mesh);

      var cube4Node = new SceneNodeContainer();
      cube4Node.Components = new List<SceneComponentContainer>();
      cube4Node.Components.Add(_cube4Transform);
      cube4Node.Components.Add(cube4Material);
      cube4Node.Components.Add(cube4Mesh);
    

      // Create the scene containing the cube as the only object
      _scene = new SceneContainer();
      _scene.Children = new List<SceneNodeContainer>();
      _scene.Children.Add(cubeNode);

      _scene.Children.Add(cube1Node);

      _scene.Children.Add(cube2Node);

      _scene.Children.Add(cube3Node);

      _scene.Children.Add(cube4Node);

      // Create a scene renderer holding the scene above
      _sceneRenderer = new SceneRenderer(_scene);
        }

        
        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
             // Clear the backbuffer
      RC.Clear(ClearFlags.Color | ClearFlags.Depth);

      // Setup the camera 
      RC.View = float4x4.CreateTranslation(0, 0, 50) * float4x4.CreateRotationY(_camAngle);

      // Animate the camera angle
      _camAngle = _camAngle + 90.0f * M.Pi/180.0f * DeltaTime;


      // Render the scene on the current render context
      _sceneRenderer.Render(RC);

      // Swap buffers: Show the contents of the backbuffer (containing the currently rendered farame) on the front buffer.
      Present();

      // Animate the cube
      _cubeTransform.Translation = new float3(0, 5 * M.Sin(3 * TimeSinceStart), 0);
      _cube1Transform.Translation = new float3(2, 1 * M.Sin(4 * TimeSinceStart), 0);
      _cube2Transform.Translation = new float3(4, 2 * M.Sin(2 * TimeSinceStart), 0);
      _cube3Transform.Translation = new float3(6, 3 * M.Sin(1 * TimeSinceStart), 0);
      _cube4Transform.Translation = new float3(8, 4 * M.Sin(5 * TimeSinceStart), 0);
        }


        // Is called when the window was resized
        public override void Resize()
        {
            // Set the new rendering area to the entire new windows size
            RC.Viewport(0, 0, Width, Height);

            // Create a new projection matrix generating undistorted images on the new aspect ratio.
            var aspectRatio = Width / (float)Height;

            // 0.25*PI Rad -> 45° Opening angle along the vertical direction. Horizontal opening angle is calculated based on the aspect ratio
            // Front clipping happens at 1 (Objects nearer than 1 world unit get clipped)
            // Back clipping happens at 2000 (Anything further away from the camera than 2000 world units gets clipped, polygons will be cut)
            var projection = float4x4.CreatePerspectiveFieldOfView(M.PiOver4, aspectRatio, 1, 20000);
            RC.Projection = projection;
        }
    }
}