# UnityShaderCollection

I plan to add various shaders in the future. Now there is a file about compute shaders.

## Visualizer.unity
This is a sample that visualizes the result of image processing using a compute shader in Unity.  
Applies specific image processing effects based on user-specified parameters.

### Features
- IShaderHandler Interface: This interface abstracts the execution of any shader.
- ComputeShaderHandler Class: This class implements the IShaderHandler interface and defines the generic behavior for shader processing.
- PixelArtHandler Class: This class inherits from ComputeShaderHandler and provides a specific implementation for pixel art processing.
- Visualizer Class: This class uses an instance of ComputeShaderHandler or its subclasses to perform specific shader processing.

### Usage
1. Open your Unity project.
1. Assign the instance of the shader handler you want to use (such as PixelArtHandler) to the _shaderHandler field of the Visualizer.cs script.
1. Run your project.