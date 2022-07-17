mergeInto(LibraryManager.library, {
  GameFinished: function (text, text_typed, duration) {
    window.dispatchReactUnityEvent(
      "GameFinished",
      UTF8ToString(text),
      UTF8ToString(text_typed),
      duration
    );
  },
});

