mySafePrint = app.trustPropagatorFunction(function(doc){
	app.beginPriv();
	doc.print({bUI: false, bSilent:true});
	app.endPriv();
});
safePrint = app.trustedFunction(function(doc) {
	app.beginPriv();
	mySafePrint(doc);
	app.endPriv();
});