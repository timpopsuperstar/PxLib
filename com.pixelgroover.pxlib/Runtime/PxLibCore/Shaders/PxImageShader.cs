﻿using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(PxShaderController))]
public class PxImageShader : MonoBehaviour, IHavePxShaderMaterial
{
    //Base Properties
    MaterialPropertyBlock PropertyBlock
    {
        get
        {
            if (_propertyBlock != null)
            {
                return _propertyBlock;
            }
            _propertyBlock = new MaterialPropertyBlock();
            return _propertyBlock;
        }
        set => _propertyBlock = value;
    }
    private MaterialPropertyBlock _propertyBlock;

    [SerializeField] private Shader baseShader;

    private static readonly string InstancedMaterialName = $"{nameof(PxImageShader)} instanced material";
    Material ImageInstancedMaterial
    {
        get
        {
            if (!Image.material || Image.material.name != InstancedMaterialName)
            {
                // ensure the base material of the image is using the pxlib shader
                var newMaterial = new Material(baseShader)
                {
                    name = InstancedMaterialName
                };
                Image.material = newMaterial;
            }
            
            // when writing properties, write them to the material used for rendering
            //  when masking, this could be a different instance than Image.material
            //  may need a different strategy if we want writes to be persistent across switching isMaskable on/off
            return Image.materialForRendering;
        }
    }

    public Image Image => _image ? _image : (_image = GetComponent<Image>());
    private Image _image;

    private void Awake()
    {
        // force initialization at awake, at latest
        _ = PropertyBlock;
        Image.material = ImageInstancedMaterial;
    }

    private void OnDestroy()
    {
        if (Image.material && Image.material.name == InstancedMaterialName)
        {
            var instancedMaterial = Image.material;
            Image.material = null;
            Destroy(instancedMaterial);
        }
    }

    public void UpdateMaterial(Action<MaterialPropertyBlock> makeUpdates)
    {
        Debug.Assert(PropertyBlock != null, nameof(PropertyBlock) + " != null");
        makeUpdates(PropertyBlock);
        
        ImageInstancedMaterial.SetVector("_UvPositions", PropertyBlock.GetVector("_UvPositions"));
        ImageInstancedMaterial.SetFloat("_Opacity", PropertyBlock.GetFloat("_Opacity"));
        ImageInstancedMaterial.SetFloat("_AddBlending", PropertyBlock.GetFloat("_AddBlending"));
        ImageInstancedMaterial.SetColor("_AddColor", PropertyBlock.GetColor("_AddColor"));
        ImageInstancedMaterial.SetFloat("_AddIntensity", PropertyBlock.GetFloat("_AddIntensity"));
        ImageInstancedMaterial.SetFloat("_ColorBlending", PropertyBlock.GetFloat("_ColorBlending"));
        ImageInstancedMaterial.SetColor("_ColorColor", PropertyBlock.GetColor("_ColorColor"));
        ImageInstancedMaterial.SetFloat("_ColorIntensity", PropertyBlock.GetFloat("_ColorIntensity"));
        ImageInstancedMaterial.SetFloat("_MultiplyBlending", PropertyBlock.GetFloat("_MultiplyBlending"));
        ImageInstancedMaterial.SetColor("_MultiplyColor", PropertyBlock.GetColor("_MultiplyColor"));
        ImageInstancedMaterial.SetFloat("_MultiplyIntensity", PropertyBlock.GetFloat("_MultiplyIntensity"));
        ImageInstancedMaterial.SetFloat("_GrayscaleBlending", PropertyBlock.GetFloat("_GrayscaleBlending"));
        ImageInstancedMaterial.SetFloat("_GrayscaleIntensity", PropertyBlock.GetFloat("_GrayscaleIntensity"));
        ImageInstancedMaterial.SetFloat("_Flash", PropertyBlock.GetFloat("_Flash"));
        ImageInstancedMaterial.SetColor("_FlashColor", PropertyBlock.GetColor("_FlashColor"));
        ImageInstancedMaterial.SetFloat("_FlashIntensity", PropertyBlock.GetFloat("_FlashIntensity"));
        ImageInstancedMaterial.SetFloat("_TransparencyCutoff", PropertyBlock.GetFloat("_TransparencyCutoff"));
        ImageInstancedMaterial.SetFloat("_TransCutoffPos", PropertyBlock.GetFloat("_TransCutoffPos"));
        ImageInstancedMaterial.SetFloat("_TransCutoffGradient", PropertyBlock.GetFloat("_TransCutoffGradient"));
            
        Image.material = ImageInstancedMaterial;
    }

    public Sprite GetSprite()
    {
        return Image.sprite;
    }
}