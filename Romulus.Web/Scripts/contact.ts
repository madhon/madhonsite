/// <reference path="typings/jquery/jquery.d.ts" />
/// <reference path="typings/jquery.validation/jquery.validation.d.ts" />

jQuery.validator.setDefaults({
    highlight: function (element: HTMLInputElement, errorClass: string, validClass: string) {
        if (element.type === 'radio') {
            this.findByName(element.name).addClass(errorClass).removeClass(validClass);
        } else {
            $(element).addClass(errorClass).removeClass(validClass);
            $(element).closest('.control-group').removeClass('success').addClass('error');
        }
    },
    unhighlight: function (element: HTMLInputElement, errorClass: string, validClass: string) {
        if (element.type === 'radio') {
            this.findByName(element.name).removeClass(errorClass).addClass(validClass);
        } else {
            $(element).removeClass(errorClass).addClass(validClass);
            $(element).closest('.control-group').removeClass('error').addClass('success');
        }
    }
});