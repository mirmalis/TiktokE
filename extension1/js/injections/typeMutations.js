if (NodeList.prototype.first != null) {
  Object.defineProperty(NodeList.prototype, 'first', {
    get: function () { return this[0]; }
  });
}
if (NodeList.prototype.last != null) {
  Object.defineProperty(NodeList.prototype, 'last', {
    get: function () { return this[this.length - 1]; }
  });
}