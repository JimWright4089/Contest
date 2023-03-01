my $lin = <> ;
chomp $lin ;
($w, $h, $b) = split " ", $lin ;
while (<>) {
   chomp ;
   push @a, [split " "] ;
}
for ($k=0; $k<$b; $k++) {
   for ($i=0; $i<$h; $i++) {
      for ($j=0; $j<$w; $j++) {
         $b[$i][$j] = ($a[($i+1)%$h][$j]+$a[$i][$j]+$a[($i+$h-1)%$h][$j]) ;
      }
   }
   for ($i=0; $i<$h; $i++) {
      for ($j=0; $j<$w; $j++) {
         $a[$i][$j] = ($b[$i][($j+1)%$w]+$b[$i][$j]+$b[$i][($j+$w-1)%$w]) ;
      }
   }
}
for ($i=0; $i<$h; $i++) {
   for ($j=0; $j<$w; $j++) {
      $seen{$a[$i][$j]}++ ;
   }
}
print((scalar keys %seen), "\n") ;
