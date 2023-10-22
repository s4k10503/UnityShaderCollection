# UnityShaderCollection

I plan to add various shaders in the future.

## Visualizer2D.unity

This is a sample that visualizes the result of image processing using a compute shader in Unity.  
Applies specific image processing effects based on user-specified parameters.

### Features

- IComputeShaderHandler Interface
  - This interface abstracts the execution of any shader.
- ComputeShaderHandler Class
  - This class implements the IComputeShaderHandler interface and defines the generic behavior for shader processing.
- ThredSize Struct
  - This structure defines the thread size of the shader.
  - The thread size is used to specify the dimension of the threads in the grid in a compute shader run.
  - This structure specifies the number of threads for the X, Y, and Z directions.
- Visualizer Class
  - This class uses an instance of ComputeShaderHandler or its subclasses to perform specific shader processing.

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

#### Twirl

This is a sample of twirl effect using TwirlEffectHandler.cs & TwirlEffect.compute.  

![lenna_twirl](https://github.com/s4k10503/UnityShaderCollection/assets/50241623/d05acaa6-15b3-497b-a6b2-69adc8ce0d24)

#### RadialBlur

This is a sample of radial blur effect using RadialBlurHandler.cs & RadialBlur.compute.

![lenna_radialblur](https://github.com/s4k10503/UnityShaderCollection/assets/50241623/580472a0-87e4-43c6-906c-8eccbdfe22b8)

#### Shake

This is a sample of shake effect using ShakeHandler.cs & shake.compute.

![lenna_shake](https://github.com/s4k10503/UnityShaderCollection/assets/50241623/8f5443c2-d223-4e21-8de9-c80e683ba85d)

### Shake2

This is a sample of shake effect using ShakeHandler.cs & shake2.compute.

<https://github.com/s4k10503/UnityShaderCollection/assets/50241623/049bdba8-cc1e-448d-ae2d-47b7ab6e5422>

### Fisheye

This is a sample of shake effect using FisheyeHandler.cs & Fisheye.compute.

<https://github.com/s4k10503/UnityShaderCollection/assets/50241623/e4bac81f-601a-4c98-b55e-943927453d88>

## Visualizer3D.unity

This is a sample project that allows for the visualization and dynamic manipulation of shaders in Unity.

### Features

- IMaterialShaderHandler Interface
  - This interface serves as a generalized contract for handling different types of shaders.
- MaterialShaderHandler Class
  - This class acts as a base class implementing the IMatesialShaderHandler, designed for generic shader manipulations.

### Usage

1. Open your Unity project.
1. Navigate to the object where you have attached a script derived from MaterialShaderHandler.
1. Assign the material you wish to manipulate to the TargetMaterial field of the script.
1. Run your project. You can now dynamically adjust the shader parameters via the script.

### Examples

#### ViewAngleTransparency

This is a sample of the ViewAngleTransparency effect using ViewAngleTransparencyHandler.cs & ViewAngleTransparency.shader.

<https://github.com/s4k10503/UnityShaderCollection/assets/50241623/5a42ad0a-f1f8-4beb-8e7d-c78e08fff225>

#### ToonWithOutline

This is a sample of the Toon effect using ToonWithOutlineyHandler.cs & ToonWithOutline.shader.

<https://github.com/s4k10503/UnityShaderCollection/assets/50241623/ecda7cb8-740a-4a1d-b62c-8f0d1e0240d4>
