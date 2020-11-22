using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SimpleAnimator))]
public class SimpleAnimatorInspector : Editor
{
    private SimpleAnimator _animator;
    private Anim _currentAnim;
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        _animator = target as SimpleAnimator;

        if (_animator.CurrentAnim)
        {
            EditorGUI.BeginChangeCheck();
            int frameCount = _animator.CurrentAnim.FrameCount;

            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField("Current Frame");            
            int frameSlider = EditorGUILayout.IntSlider(_animator.CurrentFrame, 0, frameCount - 1);
            EditorGUILayout.EndHorizontal();

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_animator, "Change current frame");
                EditorUtility.SetDirty(_animator);
                _animator.CurrentFrame = frameSlider;
                _animator.SpriteRenderer.sprite = _animator.CurrentAnim.Frames[_animator.CurrentFrame];
            }
        }
    }
}
