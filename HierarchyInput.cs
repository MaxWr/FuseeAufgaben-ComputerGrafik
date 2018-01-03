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
    public class HierarchyInput : RenderCanvas
    {
        private SceneContainer _scene;
        private SceneRenderer _sceneRenderer;
        private float _camAngle = 0;
        private TransformComponent _baseTransform;
        private TransformComponent _bodyTransform;
        private TransformComponent _upperArmTransform;
        private TransformComponent _foreArmTransform;
        private TransformComponent _finger1Transform;
        private TransformComponent _finger2Transform;
        private TransformComponent _finger3Transform;

        SceneContainer CreateScene()
        {
            // Initialize transform components that need to be changed inside "RenderAFrame"
            _baseTransform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 0, 0)
            };

            _bodyTransform = new TransformComponent
            {
                Rotation = new float3(0, 1.2f, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(0, 6, 0)
            };
            
            _upperArmTransform = new TransformComponent
            {
                Rotation = new float3(0.8f, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(2, 4, 0)
            };

            _foreArmTransform = new TransformComponent
            {
                Rotation = new float3(0.8f, 0, 0),
                Scale = new float3(1, 1, 1),
                Translation = new float3(-2, 8, 0)
            };

            _finger1Transform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(0.5f, 0.5f, 0.5f),
                Translation = new float3(-1.5f, 8, -1),
            };

             _finger2Transform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(0.5f, 0.5f, 0.5f),
                Translation = new float3(1.5f, 8, -1),
            };

            _finger3Transform = new TransformComponent
            {
                Rotation = new float3(0, 0, 0),
                Scale = new float3(0.5f, 0.5f, 0.5f),
                Translation = new float3(0, 8, 1),
            };    

            // Setup the scene graph
            return new SceneContainer
            {
                Children = new List<SceneNodeContainer>
                {
                    new SceneNodeContainer
                    {
                        Components = new List<SceneComponentContainer>
                        {
                            // TRANSFROM COMPONENT
                            _baseTransform,

                            // MATERIAL COMPONENT
                            new MaterialComponent
                            {
                                Diffuse = new MatChannelContainer { Color = new float3(0.7f, 0.7f, 0.7f) },
                                Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                            },

                            // MESH COMPONENT
                            SimpleMeshes.CreateCuboid(new float3(10, 2, 10))
                        }
                    },
                        // RED BODY
                        new SceneNodeContainer
                        {
                            Components = new List<SceneComponentContainer>
                            {
                                _bodyTransform,
                                new MaterialComponent
                                {
                                    Diffuse = new MatChannelContainer { Color = new float3(1, 0, 0) },
                                    Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                },
                                SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                            },
                            Children = new List<SceneNodeContainer>
                        {
                        // GREEN UPPER ARM
                        new SceneNodeContainer
                        {
                        Components = new List<SceneComponentContainer>
                            {
                                _upperArmTransform,
                            },
                            Children = new List<SceneNodeContainer>
                            {
                                new SceneNodeContainer
                                {
                                    Components = new List<SceneComponentContainer>
                                    {
                                        new TransformComponent
                                        {
                                            Rotation = new float3(0, 0, 0),
                                            Scale = new float3(1, 1, 1),
                                            Translation = new float3(0, 4, 0)
                                        },
                                        new MaterialComponent
                                        {
                                            Diffuse = new MatChannelContainer { Color = new float3(0, 1, 0) },
                                            Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                        },
                                        SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                    }
                                },

                                // BLUE FOREARM
                                new SceneNodeContainer
                                {
                                    Components = new List<SceneComponentContainer>
                                        {
                                            _foreArmTransform,
                                        },
                                            Children = new List<SceneNodeContainer>
                                    {
                                    new SceneNodeContainer
                                        {
                                            Components = new List<SceneComponentContainer>
                                            {
                                                new TransformComponent
                                                {
                                                    Rotation = new float3(0, 0, 0),
                                                    Scale = new float3(1, 1, 1),
                                                    Translation = new float3(0, 4, 0)
                                                },
                                                
                                                new MaterialComponent
                                                {
                                                    Diffuse = new MatChannelContainer { Color = new float3(0, 0, 1) },
                                                    Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5 }
                                                },
                                                SimpleMeshes.CreateCuboid(new float3(2, 10, 2))
                                            }
                                        },
                                        //First Finger
                                        new SceneNodeContainer
                                        {
                                            Components = new List<SceneComponentContainer>
                                            {
                                                _finger1Transform,
                                            },
                                            Children = new List<SceneNodeContainer>
                                            {
                                                new SceneNodeContainer
                                                {
                                                        Components = new List<SceneComponentContainer>
                                                    {
                                                    new TransformComponent
                                                        {
                                                            Rotation = new float3(0, 0, 0),
                                                            Scale = new float3(2, 3, 1),
                                                            Translation = new float3(0, 5, 0.5f),
                                                        },
                                                        new MaterialComponent
                                                        {
                                                            Diffuse = new MatChannelContainer { Color = new float3(1, 0, 0) },
                                                            Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5}
                                                        },
                                                        SimpleMeshes.CreateCuboid(new float3(1.25f, 3.5f, 1.75f))

                                                    }
                                                }
                                            }
                                        },
                                    
                                     //Second Finger
                                        new SceneNodeContainer
                                        {
                                            Components = new List<SceneComponentContainer>
                                            {
                                                _finger2Transform,
                                            },
                                            Children = new List<SceneNodeContainer>
                                            {
                                                new SceneNodeContainer
                                                {
                                                        Components = new List<SceneComponentContainer>
                                                    {
                                                    new TransformComponent
                                                        {
                                                            Rotation = new float3(0, 0, 0),
                                                            Scale = new float3(2, 3, 1),
                                                            Translation = new float3(0, 5, 0.5f),
                                                        },
                                                        new MaterialComponent
                                                        {
                                                            Diffuse = new MatChannelContainer { Color = new float3(1, 0, 0) },
                                                            Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5}
                                                        },
                                                        SimpleMeshes.CreateCuboid(new float3(1.25f, 3.5f, 1.75f))

                                                    }
                                                }
                                            }
                                        }, 
                                    
                                    //Third Finger
                                        new SceneNodeContainer
                                        {
                                            Components = new List<SceneComponentContainer>
                                            {
                                                _finger3Transform,
                                            },
                                            Children = new List<SceneNodeContainer>
                                            {
                                                new SceneNodeContainer
                                                {
                                                        Components = new List<SceneComponentContainer>
                                                    {
                                                    new TransformComponent
                                                        {
                                                            Rotation = new float3(0, 0, 0),
                                                            Scale = new float3(2, 3, 1),
                                                            Translation = new float3(0, 5, 0.5f),
                                                        },
                                                        new MaterialComponent
                                                        {
                                                            Diffuse = new MatChannelContainer { Color = new float3(1, 0, 0) },
                                                            Specular = new SpecularChannelContainer { Color = new float3(1, 1, 1), Shininess = 5}
                                                        },
                                                        SimpleMeshes.CreateCuboid(new float3(1.25f, 3.5f, 1.75f))

                                                    }
                                                }
                                            }
                                        }
                                    },
                                }
                            }
                        }
                    }
                }
            }
        };
    }

    
    

        // Init is called on startup. 
        public override void Init()
        {
            // Set the clear color for the backbuffer to white (100% intensity in all color channels R, G, B, A).
            RC.ClearColor = new float4(0.8f, 0.9f, 0.7f, 1);

            _scene = CreateScene();

            // Create a scene renderer holding the scene above
            _sceneRenderer = new SceneRenderer(_scene);
        }

        // RenderAFrame is called once a frame
        public override void RenderAFrame()
        {
            if(Mouse.LeftButton == true)
            {
                _camAngle -= (Mouse.Velocity.x * DeltaTime) / 100;
            }

            float bodyRot = _bodyTransform.Rotation.y;
            bodyRot += Keyboard.LeftRightAxis * DeltaTime * 3;
            _bodyTransform.Rotation = new float3(0, bodyRot, 0);

            float upperRot = _upperArmTransform.Rotation.x;
            upperRot += Keyboard.UpDownAxis * DeltaTime * 3;
            _upperArmTransform.Rotation = new float3(upperRot, 0, 0);

            float foreRot = _foreArmTransform.Rotation.x;
            foreRot += Keyboard.WSAxis * DeltaTime * 3;
            _foreArmTransform.Rotation = new float3(foreRot, 0, 0);

            float finger1 = _finger1Transform.Rotation.x;
            finger1 += Keyboard.ADAxis * DeltaTime * 3;
            _finger1Transform.Rotation = new float3(finger1, 0, 0);

            float finger2 = _finger2Transform.Rotation.x;
            finger2 += Keyboard.ADAxis * DeltaTime * 3;
            _finger2Transform.Rotation = new float3(finger2, 0, 0);

            float finger3 = _finger3Transform.Rotation.x;
            finger3 -= Keyboard.ADAxis * DeltaTime * 3;
            _finger3Transform.Rotation = new float3(finger3, 0, 0);
            
            float finger1Rot = _finger1Transform.Rotation.x;
            if(finger1Rot < -1){
                finger1Rot = -1;
            }
            if(finger1Rot > 0){
                finger1Rot = 0;
            }
            finger1Rot -= Keyboard.ADAxis * DeltaTime * 3;
            _finger1Transform.Rotation = new float3(finger1Rot, 0, 0);

            float finger2Rot = _finger2Transform.Rotation.x;
            if(finger2Rot < -1){
                finger2Rot = -1;
            }
            if(finger2Rot > 0){
                finger2Rot = 0;
            }
            finger2Rot -= Keyboard.ADAxis * DeltaTime * 3;
            _finger2Transform.Rotation = new float3(finger2Rot, 0, 0);

            float finger3Rot = _finger3Transform.Rotation.x;
            if(finger3Rot > 1){
                finger3Rot = 1;
            }
            if(finger3Rot < 0){
                finger3Rot = 0;
            }
            finger3Rot += Keyboard.ADAxis * DeltaTime * 3;
            _finger3Transform.Rotation = new float3(finger3Rot, 0, 0);


            RC.Clear(ClearFlags.Color | ClearFlags.Depth);

            RC.View = float4x4.CreateTranslation(0, -10, 50) * float4x4.CreateRotationY(_camAngle);

            _sceneRenderer.Render(RC);

            Present();
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
