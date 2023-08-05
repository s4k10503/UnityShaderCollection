# UnityShaderCollection

I plan to add various shaders in the future. Now there is a file about compute shaders.

## Visualizer.unity

This is a sample that visualizes the result of image processing using a compute shader in Unity.  
Applies specific image processing effects based on user-specified parameters.

### Features

- IShaderHandler Interface: This interface abstracts the execution of any shader.
- ComputeShaderHandler Class: This class implements the IShaderHandler interface and defines the generic behavior for shader processing.
- ThredSize Struct: This structure defines the thread size of the shader. The thread size is used to specify the dimension of the threads in the grid in a compute shader run. This structure specifies the number of threads for the X, Y, and Z directions.
- Visualizer Class: This class uses an instance of ComputeShaderHandler or its subclasses to perform specific shader processing.

### Usage

1. Open your Unity project.
1. Assign the instance of the shader handler you want to use (such as PixelArtHandler) to the _shaderHandler field of the Visualizer.cs script.
1. Run your project.

### Examples

#### Pixelart

This is a sample of piexlart effect using PixelArtHandler.cs & PixelArt.compute.  

![lenna_pixelart](https://github.com/s4k10503/UnityShaderCollection/assets/50241623/35b3c224-19d1-44f2-8700-18c367836210)

#### ImageRotation

This is a sample of rotation effect using ImageRotationHandler.cs & ImageRotationComputeShader.compute.  

![lenna_rotation](https://github.com/s4k10503/UnityShaderCollection/assets/50241623/2d9b5e49-279c-43cb-89ce-a1f0cc1bfdcf)

#### Glitch

This is a sample of glitch effect using GlitchEffectHandler.cs & GlitchEffect.compute.  

![lenna_glitch](https://github.com/s4k10503/UnityShaderCollection/assets/50241623/95e784d5-3603-4896-a61c-9ad4a1ac3779)

