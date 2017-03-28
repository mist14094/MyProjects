trustedSilentPrint = app.trustedFunction( function (_thisdoc) {
app.beginPriv();
var pp = _thisdoc.getPrintParams();
pp.interactive = pp.constants.interactionLevel.silent;
_thisdoc.print(pp);
app.endPriv();
})