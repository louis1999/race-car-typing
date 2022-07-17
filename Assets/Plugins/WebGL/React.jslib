mergeInto(LibraryManager.library, {
  GameFinished: function (score) {
    window.dispatchReactUnityEvent(
      "GameFinished",
      score
    );
  },
});

