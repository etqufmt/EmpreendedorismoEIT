/*
 * Localized default methods for the jQuery validation plugin.
 * Locale: PT_BR
 */
function replaceComma(number) {
	return number.replace(".", "").replace(",", ".");
}

jQuery.extend(jQuery.validator.methods, {
	number: function (value, element) {
		return this.optional(element) || /^-?(?:\d+|\d{1,3}(?:\.\d{3})+)(?:,\d+)?$/.test(value);
	},
	min: function (value, element, param) {
		value = replaceComma(value);
		return this.optional(element) || value >= param;
	},
	max: function( value, element, param ) {
		value = replaceComma(value);
		return this.optional(element) || value <= param;
	},
	range: function( value, element, param ) {
		value = replaceComma(value);
		return this.optional(element) || (value >= param[0] && value <= param[1]);
	},
	step: function( value, element, param ) {
		value = replaceComma(value);
		var type = $(element).attr("type"),
			errorMessage = "Step attribute on input type " + type + " is not supported.",
			supportedTypes = [ "text", "number", "range" ],
			re = new RegExp( "\\b" + type + "\\b" ),
			notSupported = type && !re.test( supportedTypes.join() ),
			decimalPlaces = function( num ) {
				var match = ( "" + num ).match( /(?:\.(\d+))?$/ );
				if ( !match ) {
					return 0;
				}

				// Number of digits right of decimal point.
				return match[ 1 ] ? match[ 1 ].length : 0;
			},
			toInt = function( num ) {
				return Math.round( num * Math.pow( 10, decimals ) );
			},
			valid = true,
			decimals;

		// Works only for text, number and range input types
		// TODO find a way to support input types date, datetime, datetime-local, month, time and week
		if ( notSupported ) {
			throw new Error( errorMessage );
		}

		decimals = decimalPlaces( param );

		// Value can't have too many decimals
		if ( decimalPlaces( value ) > decimals || toInt( value ) % toInt( param ) !== 0 ) {
			valid = false;
		}

		return this.optional( element ) || valid;
	}
});

jQuery.extend(jQuery.validator.messages, {
    required: "Este campo &eacute; requerido.",
    remote: "Por favor, corrija este campo.",
    email: "Email inv&aacute;lido",
    url: "URL inv&aacute;lida",
    date: "Por favor, forne&ccedil;a uma data v&aacute;lida.",
    dateISO: "Por favor, forne&ccedil;a uma data v&aacute;lida (ISO).",
    number: "Por favor, forne&ccedil;a um n&uacute;mero v&aacute;lido.",
    digits: "Por favor, forne&ccedil;a somente d&iacute;gitos.",
    equalTo: "Por favor, forne&ccedil;a o mesmo valor novamente.",
    maxlength: jQuery.validator.format("Por favor, forne&ccedil;a n&atilde;o mais que {0} caracteres."),
    minlength: jQuery.validator.format("Por favor, forne&ccedil;a ao menos {0} caracteres."),
    rangelength: jQuery.validator.format("Por favor, forne&ccedil;a um valor entre {0} e {1} caracteres."),
    range: jQuery.validator.format("Por favor, forne&ccedil;a um valor entre {0} e {1}."),
    max: jQuery.validator.format("Por favor, forne&ccedil;a um valor menor ou igual a {0}."),
    min: jQuery.validator.format("Por favor, forne&ccedil;a um valor maior ou igual a {0}."),
	step: $.validator.format( "Por favor, forne&ccedil;a um m&uacute;ltiplo de {0}." )
});
