my %remap = ('middle'=>2, 'upper'=>1, 'lower'=>3) ;
scalar <> ;
while (<>) {
   chomp ;
   s/ class// ;
   s/:// ;
   @f = split " ", $_ ;
   $name = shift @f ;
   $key = join '', map { $remap{$_} } reverse @f ;
   $key .= '2' while length($key) < 100 ;
   push @a, "$key $name" ;
}
print((split " ")[1], "\n") for sort @a ;
