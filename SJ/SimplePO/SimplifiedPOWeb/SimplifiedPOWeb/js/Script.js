(function (global, undefined) {
    var demo = {};

    function alertCallBackFn(arg) {
        radalert("<strong>radalert</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Result");
    }

    function confirmCallBackFn(arg) {
        radalert("<strong>radconfirm</strong> returned the following result: <h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Result");
    }

    function promptCallBackFn(arg) {
        radalert("After 7.5 million years, <strong>Deep Thought</strong> answers:<h3 style='color: #ff0000;'>" + arg + "</h3>", 350, 250, "Deep Thought");
    }

    Sys.Application.add_load(function () {
        //attach a handler to radio buttons to update global variable holding image url
        $telerik.$('input:radio').bind('click', function () {
            demo.imgUrl = $telerik.$(this).val();
        });
    });

    global.alertCallBackFn = alertCallBackFn;
    global.confirmCallBackFn = confirmCallBackFn;
    global.promptCallBackFn = promptCallBackFn;

    global.$dialogsDemo = demo;
})(window);