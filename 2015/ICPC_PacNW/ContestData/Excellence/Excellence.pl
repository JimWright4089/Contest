scalar <> ;
while (<>) {
   chomp ;
   push @a, $_ ;
}
@a = sort { $a <=> $b } @a ;
$min = $a[0] + $a[@a-1] ;
for ($i=0; $i<@a; $i++) {
   $t = $a[$i] + $a[@a-$i-1] ;
   $min = $t if $t < $min ;
}
print "$min\n" ;
