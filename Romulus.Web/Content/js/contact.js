 $(document).ready(function() {
            $('#frmContact').bootstrapValidator({
                message: 'This value is not valid',
                feedbackIcons: {
                    valid: 'glyphicon glyphicon-ok',
                    invalid: 'glyphicon glyphicon-remove',
                    validating: 'glyphicon glyphicon-refresh'
                },
                fields: {
                    Name: {
                        message: 'Enter your name',
                        validators: {
                            notEmpty: {
                                message: 'Enter your name'
                            },
                            stringLength: {
                                min: 1,
                                max: 60,
                                message: 'Your name must be more than 6 and less than 60 characters long'
                            },
                            regexp: {
                                regexp: /^[a-zA-Z0-9_]+$/,
                                message: 'The username can only consist of alphabetical, number and underscore'
                            }
                        }
                    },
                    Email: {
                        validators: {
                            notEmpty: {
                                message: 'The email is required and cannot be empty'
                            },
                            emailAddress: {
                                message: 'The input is not a valid email address'
                            }
                        }
                    },
                    Message: {
                        message: 'Enter your message',
                        validators: {
                            notEmpty: {
                                message: 'Enter your message'
                            }
                        }
                    },
                }
            });
        });
		