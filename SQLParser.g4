lexer grammar SQLLexer;

// KEYWORDS
AS					: 'AS' ;
EXISTS              : 'EXISTS' ;
FROM                : 'FROM' ;
IN					: 'IN' ;
LIKE				: 'LIKE' ;
OBJECT				: 'OBJECT' ;
REF					: 'REF' ;
SELECT              : 'SELECT' ;
WHERE				: 'WHERE' ;

COMMA				: ',';
EQUAL				: '=';
NOT_EQUAL			: '<>';
DOT					: '.';
OPEN_PAR			: '(';
CLOSE_PAR			: ')';
IDENTIFIER          : [A-Za-z_] [A-Za-z_0-9]*;
WHITESPACE          : (' '|'\t')+ -> skip ;
NEWLINE             : ('\r'? '\n' | '\r')+ -> skip ;
DECIMAL				: DEC_DIGIT+;
STRING				: 'N'? '\'' (~'\'' | '\'\'')* '\'';

fragment DEC_DIGIT:    [0-9];