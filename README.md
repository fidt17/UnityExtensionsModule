# UnityExtensionsModule
## Version : 1.0.5

## Installation
1) Unity Package Manager
2) Add package from git URL:
3) https://github.com/fidt17/UnityExtensionsModule.git?path=/Assets/fidt17/UnityExtensionsModule

## Extensions

### Collection Extensions
```css
  # T SampleOne<T>(this IReadOnlyList<T> collection)
  # IEnumerable<T> Sample<T>(this IReadOnlyList<T> collection, int N)
  # IEnumerable<T> SampleUnique<T>(this IReadOnlyList<T> collection, int N)
  # void Shuffle<T>(this IList<T> collection)
```
  
### Layer Extensions
```css
  # bool Contains(this LayerMask mask, int layer)
  # void SetLayerRecursive(this GameObject gObj, int layer)
```

### Math Extensions
```css
  # float AdvSign(float value)
  # float Remap(float value, float low1, float high1, float low2, float high2)
```

### Vector Extensions
```css
  # Vector2 RandomOffset(Vector2 origin, float radius)
  # Vector3 RandomOffset(Vector3 origin, float radius)
```
   
### Tween Extensions
```css
  # IEnumerator Progress(float duration, Action<float> progressAction, Func<IEnumerator> yieldFunc = null)
  # IEnumerator ReverseProgress(float duration, Action<float> progressAction, Func<IEnumerator> yieldFunc = null)
  
  # IEnumerator Move(Transform transform, Vector3 startPosition, Vector3 targetPosition, float duration)
  # IEnumerator Move(Transform transform, Vector3 targetPosition, float duration)
  
  # IEnumerator Scale(Transform transform, Vector3 start, Vector3 target, float duration)
  # IEnumerator Scale(Transform transform, Vector3 target, float duration)
  # IEnumerator ScaleY(Transform transform, float target, float duration)
  
  # IEnumerator Color(Color startColor, Color endColor, float duration, Action<Color> progressAction)
  # IEnumerator Color(SpriteRenderer spriteRenderer, Color targetColor, float duration)
  # IEnumerator Color(SpriteRenderer spriteRenderer, Color startColor, Color targetColor, float duration)
  # IEnumerator Color(Image image, Color targetColor, float duration)
  # IEnumerator Color(Image image, Color startColor, Color targetColor, float duration)
```
  
### Color Extensions
```css
  # Color GenerateRandomColor(float alpha = 1)
```
  
### Debug Draw Extensions
```css
  # void DrawCross(Vector3 position, float size, Color color, float duration = 0)
  # void DrawDiagonalCross(Vector2 position, float size, Color color, float duration = 0)
  # void DrawRect(Rect rect, Color color, float t = 0)
  # void DrawCircle(Vector3 position, float radius, Color color, float duration = 0)
```

## Extended Coroutines

### Coroutine Extensions
```css
  # IEnumerator SetOnComplete(this IEnumerator enumerator, Action action)
  # IEnumerator WaitForFrames(int N)
  # IEnumerator Delay(float seconds, Action action)
```
  
### CoroutineRunner
  Simple singleton to run coroutines with.
  Calling CoroutineRunner.Istance will create a new GameObject if needed.
```css
  CoroutineRunner.Instance.StartCoroutine(...);
```
  Can destroy if needed
```css
  CoroutineRunner.Destroy()
```

### ExCoroutine
  Extended version of Unity's Coroutine.
  Keeps track of already running coroutines and automaticaly stops them on restart ("singleton coroutine").
  Can control execution of multiple coroutines at once (sequentially or "in parallel")
  
### ExCoroutine Extensions
```css
  # ExCoroutine StartExCoroutine(this MonoBehaviour owner, IEnumerator enumerator)
  # WaitForExCoroutine WaitFor(this ExCoroutine exCoroutine)
```
  
### WaitForExCoroutine
  Custom yield instruction that will wait untill ExCoroutine finished it's execution
```css
  private IEnumerator ie()
  {
      ExCoroutine exCoroutine = new ExCoroutine(CoroutineRunner.Instance);
      yield return new WaitForExCoroutine(exCoroutine);
  }
```
    
## Extended Event Handling

### WaitForEvent
  Event handler that listens for an event invocation only once.
  Can be used as yield instruction in Coroutines
```css
    private IEnumerator ie()
    {
      var unityEvent = new UnityEvent();
      yield return new WaitForEvent(unityEvent);
    }
```
  
### OneTimeEventCallback
  - Callbacks for WaitForEvent
  
### OneTimeMultipleEventsCallback
  - Callbacks for multiple WaitForEvent
  - Can specify listen method: All, Any
