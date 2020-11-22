using UnityEngine;
using UnityEditor;
using System.Collections;


class CustomPostprocessor : AssetPostprocessor
{
	// audio asset preprocessor
	void OnPreprocessAudio()
	{
		// check if it's a music file
		if (assetPath.StartsWith("Assets/Music"))
		{
			PreprocessMusic();
		}
	}

	// texture asset preprocessor
	void OnPreprocessTexture()
	{
		// check if it's a platform image (icons, splash, etc.)
		if (assetPath.StartsWith("Assets/Platform"))
		{
			PreprocessPlatformImages();
		}

		// check if it's a sprite source image
		if (assetPath.StartsWith("Assets/Sprites/Sources"))
		{
			PreprocessSpriteSource();
		}

		// check if it's a sprite texture atlas
		if (assetPath.StartsWith("Assets/Graphics"))
		{
			PreprocessSpriteAtlas();
		}
	}


	// preprocess music (2D, stream from disc, hardware decoding)
	void PreprocessMusic()
	{
		AudioImporter importer = (AudioImporter)assetImporter;
	}


	// preprocess platform icons, etc.
	void PreprocessPlatformImages()
	{
		TextureImporter importer = (TextureImporter)assetImporter;
	}


	// preprocess sprite source images (uncompressed, no mips, non-pow2, etc.)
	void PreprocessSpriteSource()
	{
		TextureImporter importer = (TextureImporter)assetImporter;
	}


	// preprocess sprite texture atlases (uncompressed, mips, POT, filter, etc.)
	void PreprocessSpriteAtlas()
	{
		TextureImporter importer = (TextureImporter)assetImporter;
		importer.textureCompression = TextureImporterCompression.Uncompressed;
		importer.filterMode = FilterMode.Point;
		importer.spritePixelsPerUnit = 1;
	}
}