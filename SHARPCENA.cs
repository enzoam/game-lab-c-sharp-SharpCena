using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SharpDX;
using SharpDX.Toolkit;
using SharpDX.Toolkit.Graphics;
using SharpDX.Toolkit.Audio;
using SharpDX.Toolkit.Input;
using SharpDX.Direct3D11;
using SharpDX.DXGI;
using System.Diagnostics;
using SharpDX.D3DCompiler;
using SharpDX.Direct3D;
using SharpDX.Windows;
using MapFlags = SharpDX.Direct3D11.MapFlags;
using Buffer = SharpDX.Direct3D11.Buffer;
using Device = SharpDX.Direct3D11.Device;
using System.Threading.Tasks;
using SharpDX.IO;

namespace SHARPCENA
{

    public class SHARPCENA : Game
    {
        // 

        private GraphicsDeviceManager graphicsDeviceManager;
        private SpriteBatch spriteBatch;
        private SpriteFont arial16Font;

        private Matrix view;
        private Matrix projection;

        private BasicEffect basicEffectPrimitive1, basicEffectPrimitive2, basicEffectPrimitive3, basicEffectPrimitive4, basicEffectPrimitive5, basicEffectPrimitive6, basicEffectPrimitive7, basicEffectpilar1, basicEffectpilar2;
        private GeometricPrimitive primitive;
        private GeometricPrimitive primitive2;
        private GeometricPrimitive primitive3;
        private GeometricPrimitive primitive4;
        private GeometricPrimitive primitive5;
        private GeometricPrimitive primitive6;
        private GeometricPrimitive primitive7;
        private GeometricPrimitive pilar1;
        private GeometricPrimitive pilar2;

        private SharpDX.Toolkit.Graphics.Texture2D primitiveTexture;
        private SharpDX.Toolkit.Graphics.Texture2D primitive2Texture;
        private SharpDX.Toolkit.Graphics.Texture2D primitive3Texture;
        private SharpDX.Toolkit.Graphics.Texture2D primitive4Texture;
        private SharpDX.Toolkit.Graphics.Texture2D primitive5Texture;
        private SharpDX.Toolkit.Graphics.Texture2D primitive6Texture;
        private SharpDX.Toolkit.Graphics.Texture2D primitive7Texture;
        private SharpDX.Toolkit.Graphics.Texture2D primitivepilar1Texture;
        private SharpDX.Toolkit.Graphics.Texture2D primitivepilar2Texture;

        private SoundEffect SomTiro;
        private SoundEffect SomAcerto;
        private SoundEffect SomErro;
        private SoundEffect SomMusica;

        private KeyboardManager keyboard;
        private KeyboardState keyboardState;

        

        float var_direcao = 0.0f;

        float var_altura_tiro = 5.0f;
        float var_distancia_tiro = 0.0f;
        float var_direcao_tiro = 0.0f;
        float var_tiro = 0.0f;
        float velocidade_vaivem = 1.0f;
        int var_score = 0;
        int var_erros;
        int var_acertos;

        float giro = 1.0f;

        //private AudioManager tocasom;

        //Primitive_trx = 
        //Primitive_try = 
        //Primitive_trz = 

        float var_pulse = 0.0f;
        int var_tamanho = 0;

        float var_vaivem_pos = 0f;
        float var_vaivem = 1.0f;

        public Color4 color;

        

        public SHARPCENA()
        {
            graphicsDeviceManager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            keyboard = new KeyboardManager(this);

        }

        protected override void Initialize()
        {
            // Título da Tela
            Window.Title = "SHARPCENA";
            base.Initialize();
        }

        protected override void LoadContent()
        {
           
            spriteBatch = ToDisposeContent(new SpriteBatch(GraphicsDevice));

            // Sprite Font
            // [Arial16.xml] Padrão do Sharp DX
            arial16Font = Content.Load<SpriteFont>("Arial16");

            // EfeitoBásico Objeto Primitiva 1
            basicEffectPrimitive1 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectPrimitive1.PreferPerPixelLighting = true;
            basicEffectPrimitive1.Alpha = 1;
            basicEffectPrimitive1.EnableDefaultLighting();
            basicEffectPrimitive1.TextureEnabled = true;

            // EfeitoBásico Objeto Primitiva 2
            basicEffectPrimitive2 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectPrimitive2.PreferPerPixelLighting = true;
            basicEffectPrimitive2.EnableDefaultLighting();
            basicEffectPrimitive2.TextureEnabled = true;

            // EfeitoBásico Objeto Primitiva 3
            basicEffectPrimitive3 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectPrimitive3.PreferPerPixelLighting = true;
            basicEffectPrimitive3.EnableDefaultLighting();
            basicEffectPrimitive3.TextureEnabled = true;

            // EfeitoBásico Objeto Primitiva 4
            basicEffectPrimitive4 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectPrimitive4.PreferPerPixelLighting = true;
            basicEffectPrimitive4.EnableDefaultLighting();
            basicEffectPrimitive4.TextureEnabled = true;

            // EfeitoBásico Objeto Primitiva 5
            basicEffectPrimitive5 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectPrimitive5.PreferPerPixelLighting = true;
            basicEffectPrimitive5.EnableDefaultLighting();
            basicEffectPrimitive5.TextureEnabled = true;

            // EfeitoBásico Objeto Primitiva 6
            basicEffectPrimitive6 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectPrimitive6.PreferPerPixelLighting = true;
            basicEffectPrimitive6.EnableDefaultLighting();
            basicEffectPrimitive6.TextureEnabled = true;

            // EfeitoBásico Objeto Primitiva 7
            basicEffectPrimitive7 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectPrimitive7.PreferPerPixelLighting = true;
            basicEffectPrimitive7.EnableDefaultLighting();
            basicEffectPrimitive7.TextureEnabled = true;

            // PILAR 1
            basicEffectpilar1 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectpilar1.PreferPerPixelLighting = true;
            basicEffectpilar1.EnableDefaultLighting();
            basicEffectpilar1.TextureEnabled = true;

            // PILAR 2
            basicEffectpilar2 = ToDisposeContent(new BasicEffect(GraphicsDevice));
            basicEffectpilar2.PreferPerPixelLighting = true;
            basicEffectpilar2.EnableDefaultLighting();
            basicEffectpilar2.TextureEnabled = true;

            // Cria Objetos
            primitive = ToDisposeContent(GeometricPrimitive.Cylinder.New(GraphicsDevice));
            primitive2 = ToDisposeContent(GeometricPrimitive.Torus.New(GraphicsDevice));
            primitive3 = ToDisposeContent(GeometricPrimitive.Sphere.New(GraphicsDevice));
            primitive4 = ToDisposeContent(GeometricPrimitive.Cylinder.New(GraphicsDevice));
            primitive5 = ToDisposeContent(GeometricPrimitive.Cylinder.New(GraphicsDevice));
            primitive6 = ToDisposeContent(GeometricPrimitive.Torus.New(GraphicsDevice));
            primitive7 = ToDisposeContent(GeometricPrimitive.Cylinder.New(GraphicsDevice));
            pilar1 = ToDisposeContent(GeometricPrimitive.Cylinder.New(GraphicsDevice));
            pilar2 = ToDisposeContent(GeometricPrimitive.Cylinder.New(GraphicsDevice));

            primitiveTexture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("CANHAO"); // CANHAO
            primitive2Texture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("RODA"); // RODA1
            primitive3Texture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("BALA"); // BALA
            primitive4Texture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("CHAO"); // CHAO
            primitive5Texture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("ALVO"); // ALVO
            primitive6Texture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("RODA"); // RODA2
            primitive7Texture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("UNIVERSO"); // HORIZONTE
            primitivepilar1Texture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("COLUNA"); // Pilar 1
            primitivepilar2Texture = Content.Load<SharpDX.Toolkit.Graphics.Texture2D>("COLUNA"); // Pilar 2

            //SomAcerto = Content.Load<SoundEffect>("Acerto"); // SOM ACERTO
            //SomErro   = Content.Load<SoundEffect>("Acerto"); // SOM ERRO
            //SomTiro   = Content.Load<SoundEffect>("SOMTIRO"); // SOM TIRO
            //SomMusica = Content.Load<SoundEffect>("Acerto"); // MUSICA

            

            base.LoadContent();
          
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            // Visão do mundo baseada no tamanho do modelo
            view = Matrix.LookAtRH(new Vector3(0.0f, 0.0f, 7.0f), new Vector3(0, 0.0f, 0), Vector3.UnitY);
            projection = Matrix.PerspectiveFovRH(0.9f, (float)GraphicsDevice.BackBuffer.Width / GraphicsDevice.BackBuffer.Height, 0.1f, 100.0f);

            // Efeitos básicos dos Objetos Primitivos
            basicEffectPrimitive1.View = view;
            basicEffectPrimitive1.Projection = projection;

            basicEffectPrimitive2.View = view;
            basicEffectPrimitive2.Projection = projection;

            basicEffectPrimitive3.View = view;
            basicEffectPrimitive3.Projection = projection;

            basicEffectPrimitive4.View = view;
            basicEffectPrimitive4.Projection = projection;

            basicEffectPrimitive5.View = view;
            basicEffectPrimitive5.Projection = projection;

            basicEffectPrimitive6.View = view;
            basicEffectPrimitive6.Projection = projection;

            basicEffectPrimitive7.View = view;
            basicEffectPrimitive7.Projection = projection;

            basicEffectpilar1.View = view;
            basicEffectpilar1.Projection = projection;

            basicEffectpilar2.View = view;
            basicEffectpilar2.Projection = projection;

            // Recupera o estado atual do teclado
            keyboardState = keyboard.GetState();
        }

        protected override void Draw(GameTime gameTime)
        {
            // Use tempo diretamente em segundos
            var time = (float)gameTime.TotalGameTime.TotalSeconds;

            // Limpa a tela com a cor selecionada
            GraphicsDevice.Clear(Color.Black);

            //float translateX = 0.0f;


            // ------------------------------------------------------------------------
            // Desenha as primitivas usando dos efeitos pré carregados
            // ------------------------------------------------------------------------
            basicEffectPrimitive1.Texture = primitiveTexture;
            basicEffectPrimitive1.World = Matrix.Scaling(0.5f, 2.0f, 0.5f) * // CANHAO
                                Matrix.RotationX(-1.3f) *
                                Matrix.RotationY(0f) *
                                Matrix.RotationZ(0f) *
                                Matrix.Translation(var_direcao, var_pulse -1.0f, 3.5f);


            basicEffectPrimitive2.Texture = primitive2Texture;
            basicEffectPrimitive2.World = Matrix.Scaling(1.0f, 1.0f, var_pulse + 1.0f) * // RODA 1
                    Matrix.RotationX(0.0f) *
                    Matrix.RotationY(time * 0.5f) *
                    Matrix.RotationZ(1.6f) *
                    Matrix.Translation(var_direcao - 0.4f, -1.3f, 4.0f);

            basicEffectPrimitive3.Texture = primitive3Texture;
            basicEffectPrimitive3.World = Matrix.Scaling(var_pulse + 0.3f, var_pulse + 0.3f, var_pulse + 0.3f) * // BALA
                    Matrix.RotationX(1.6f) *
                    Matrix.RotationY(0f) *
                    Matrix.RotationZ(0f) *
                    Matrix.Translation(var_direcao_tiro, var_altura_tiro, var_distancia_tiro);

            basicEffectPrimitive4.Texture = primitive4Texture;
            basicEffectPrimitive4.World = Matrix.Scaling(10f, 15.0f, 10f) * // CHAO
                    Matrix.RotationX(0f) *
                    Matrix.RotationY(time * 0.3f) *
                    Matrix.RotationZ(-1.56f) *
                    Matrix.Translation(0.0f, -7f, 0.0f);

            basicEffectPrimitive5.Texture = primitive5Texture;
            basicEffectPrimitive5.World = Matrix.Scaling(var_pulse + 1.0f, var_pulse + 0.0f, var_pulse + 1.0f) * // ALVO
                    Matrix.RotationX(time * giro) *
                    Matrix.RotationY(0f) *
                    Matrix.RotationZ(1.6f) *
                    Matrix.Translation(var_vaivem * velocidade_vaivem, 0.0f, -1.0f);

            basicEffectPrimitive6.Texture = primitive6Texture;
            basicEffectPrimitive6.World = Matrix.Scaling(1.0f, 1.0f, var_pulse + 1.0f) * // RODA 2
                    Matrix.RotationX(0.0f) *
                    Matrix.RotationY(time * 0.5f) *
                    Matrix.RotationZ(1.6f) *
                    Matrix.Translation(var_direcao + 0.4f, -1.3f, 4.0f);

            basicEffectPrimitive7.Texture = primitive7Texture;
            basicEffectPrimitive7.World = Matrix.Scaling(35f, 0.1f, 35f) * // HORIZONTE
                    Matrix.RotationX(1.56f) *
                    Matrix.RotationY(0f) *
                    Matrix.RotationZ(time * 0.05f) *
                    Matrix.Translation(0f, 0f, -8f);

            basicEffectpilar1.Texture = primitivepilar1Texture;
            basicEffectpilar1.World = Matrix.Scaling(2f, 15.0f, 2f) * // PILAR 1
                    Matrix.RotationX(time * 0.3f) *
                    Matrix.RotationY(0f) *
                    Matrix.RotationZ(0f) *
                    Matrix.Translation(-4f, -7f, 0.0f);

            basicEffectpilar2.Texture = primitivepilar2Texture;
            basicEffectpilar2.World = Matrix.Scaling(2f, 17.0f, 2f) * // PILAR 2
                    Matrix.RotationX(time * 0.3f) *
                    Matrix.RotationY(0f) *
                    Matrix.RotationZ(0f) *
                    Matrix.Translation(4f, -7f, 0.0f);

            primitive.Draw(basicEffectPrimitive1);
            primitive2.Draw(basicEffectPrimitive2);
            primitive3.Draw(basicEffectPrimitive3);
            primitive4.Draw(basicEffectPrimitive4);
            primitive5.Draw(basicEffectPrimitive5);
            primitive6.Draw(basicEffectPrimitive6);
            primitive7.Draw(basicEffectPrimitive7);
            pilar1.Draw(basicEffectpilar1);
            pilar2.Draw(basicEffectpilar2);


            if (var_direcao_tiro > var_vaivem - 0.5 && var_direcao_tiro < var_vaivem + 0.5 && var_distancia_tiro < -1.0 && var_distancia_tiro > -2.0)
            {
                giro = 20.0f;
                var_tiro = 0;
                var_direcao_tiro = 0.0f;
                var_distancia_tiro = 0.0f;
                var_altura_tiro = 5.0f;
                GraphicsDevice.Clear(Color.LightYellow);
                velocidade_vaivem += 0.1f;
                var_score += 100;
                var_acertos += 1;
            }
            if (giro > 1.0f) { giro = giro - 0.1f; }

            // ------------------------------------------------------------------------
            // Mostra o texto
            // ------------------------------------------------------------------------
            spriteBatch.Begin();

            var text = new StringBuilder("Disciplina de jogos 3D 1 - PUC-PR 2014 - ENZO A. MARCHIORATO").AppendLine();
            text.Append("SCORE : ");
            text.Append(var_score);
            text.Append("  ERROS : ");
            text.Append(var_erros);
            text.Append("  ACERTOS : ");
            text.Append(var_acertos).AppendLine();
            text.Append("INSTRUCOES:").AppendLine();
            text.Append("Pressione as SETAS < e > do teclado para movimentar o canhao :").AppendLine();
            // Mostra as teclas pressionadas e atribui eventos a elas

            if (keyboardState.IsKeyDown(Keys.Left) == true)
            {
                text.Append("<").AppendLine();
                if (var_direcao > -3.0f) {var_direcao -= 0.01f;}
            }
            if (keyboardState.IsKeyDown(Keys.Right) == true)
            {
                text.Append(">").AppendLine();
                if (var_direcao < 3.0f) {var_direcao += 0.01f; }
            }

            text.Append("Pressione a tecla ESPACO para atirar :").AppendLine();

            if (keyboardState.IsKeyDown(Keys.Space) == true && var_tiro == 0)
            {
                var_tiro = 1;
                //SomTiro.Play(1.0f,1.0f,1.0f);
            }
            if (var_tiro == 1)
            {
                if (var_altura_tiro == 5.0f) { var_altura_tiro = 0.0f; }
                text.Append("BUUUUMM").AppendLine();
                if (var_direcao_tiro == 0.0f)
                {
                    var_direcao_tiro = var_direcao;
                    var_distancia_tiro = 1.0f;
                }
                if (var_distancia_tiro > -10.0f)
                {
                    if (var_distancia_tiro > -6.0f) { var_altura_tiro += 0.01f; }
                    if (var_distancia_tiro < -6.0f) { var_altura_tiro -= 0.01f; }
                    text.Append(var_distancia_tiro).AppendLine();
                }
                else
                {
                    var_tiro = 0;
                    var_direcao_tiro = 0.0f;
                    var_distancia_tiro = 0.0f;
                    var_altura_tiro = 5.0f;
                    GraphicsDevice.Clear(Color.LightYellow);
                    var_erros += 1;
                }
                var_distancia_tiro -= 0.1f;

            }

            if (var_pulse < 0.1f && var_tamanho == 0) { var_pulse += 0.002f; }
            if (var_pulse > 0.0f && var_tamanho == 1) { var_pulse -= 0.002f; }

            if (var_pulse > 0.1f) { var_tamanho = 1; }
            if (var_pulse < 0.0f) { var_tamanho = 0; }

            if (var_vaivem_pos == 0) { var_vaivem += 0.05f; }
            if (var_vaivem_pos == 1) { var_vaivem -= 0.05f; }

            if (var_vaivem >  3.0f) { var_vaivem_pos = 1; }
            if (var_vaivem < -3.0f) { var_vaivem_pos = 0; }


            spriteBatch.DrawString(arial16Font, text.ToString(), new Vector2(16, 16), Color.Black);
            spriteBatch.End();

            base.Draw(gameTime);
        }

     /*   public virtual void Initialize(DeviceManager devices)
        {
            // Remove previous buffer
            SafeDispose(ref constantBuffer);

            // Setup local variables
            var d3dDevice = devices.DeviceDirect3D;
            var d3dContext = devices.ContextDirect3D;

            var path = Windows.ApplicationModel.Package.Current.InstalledLocation.Path;

            // Compile Vertex and Pixel shaders
            // Because d3dcompiler_44.dll is not in the path, use precompiled fx files
            // var vertexShaderByteCode = ShaderBytecode.CompileFromFile("MiniCube.fx", "VS", "vs_4_0", ShaderFlags.None, EffectFlags.None);
            // vertexShaderByteCode.Save("MiniCube_VS.fxo");
            ShaderBytecode vertexShaderByteCode;
            using (var stream = new NativeFileStream(path + "\\MiniCube_VS.fxo", NativeFileMode.Open, NativeFileAccess.Read))
                vertexShaderByteCode = ShaderBytecode.Load(stream);
            vertexShader = new VertexShader(d3dDevice, vertexShaderByteCode);

            // Because d3dcompiler_44.dll is not in the path, use precompiled fx files
            // var pixelShaderByteCode = ShaderBytecode.CompileFromFile("MiniCube.fx", "PS", "ps_4_0", ShaderFlags.None, EffectFlags.None);
            // pixelShaderByteCode.Save("MiniCube_PS.fxo");
            ShaderBytecode pixelShaderByteCode;
            using (var stream = new NativeFileStream(path + "\\MiniCube_PS.fxo", NativeFileMode.Open, NativeFileAccess.Read))
                pixelShaderByteCode = ShaderBytecode.Load(stream);
            pixelShader = new PixelShader(d3dDevice, pixelShaderByteCode);

            // Layout from VertexShader input signature
            layout = new InputLayout(d3dDevice, vertexShaderByteCode, new[]
                    {
                        new InputElement("POSITION", 0, Format.R32G32B32A32_Float, 0, 0),
                        new InputElement("COLOR", 0, Format.R32G32B32A32_Float, 16, 0)
                    });

            // Instantiate Vertex buffer from vertex data
            var vertices = SharpDX.Direct3D11.Buffer.Create(d3dDevice, BindFlags.VertexBuffer, new[]
                                  {
                                      new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f), // Front
                                      new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 0.0f, 1.0f),

                                      new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f), // BACK
                                      new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 0.0f, 1.0f),

                                      new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f), // Top
                                      new Vector4(-1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, 1.0f,  1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, 1.0f, -1.0f,  1.0f), new Vector4(0.0f, 0.0f, 1.0f, 1.0f),

                                      new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f), // Bottom
                                      new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4(-1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,-1.0f, -1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),
                                      new Vector4( 1.0f,-1.0f,  1.0f,  1.0f), new Vector4(1.0f, 1.0f, 0.0f, 1.0f),

                                      new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f), // Left
                                      new Vector4(-1.0f, -1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f, -1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f,  1.0f,  1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),
                                      new Vector4(-1.0f,  1.0f, -1.0f, 1.0f), new Vector4(1.0f, 0.0f, 1.0f, 1.0f),

                                      new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f), // Right
                                      new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, -1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f, -1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f, -1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                                      new Vector4( 1.0f,  1.0f,  1.0f, 1.0f), new Vector4(0.0f, 1.0f, 1.0f, 1.0f),
                            });

            vertexBufferBinding = new VertexBufferBinding(vertices, Utilities.SizeOf<Vector4>() * 2, 0);

            // Create Constant Buffer
            constantBuffer = ToDispose(new SharpDX.Direct3D11.Buffer(d3dDevice, Utilities.SizeOf<Matrix>(), ResourceUsage.Default, BindFlags.ConstantBuffer, CpuAccessFlags.None, ResourceOptionFlags.None, 0));

            clock = new Stopwatch();
            clock.Start();
        }
    */
    }
}
