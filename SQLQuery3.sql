	SELECT d.d as [����],
		s.store_name as [������],
		g.group_name as [������ ������],
		g.good_name as [������������],
		sum(f.quantity) as [������� ��.],
		sum(f.sale_grs) as [������� ���., � ���],
		sum(f.cost_grs) as [������� ���., � ���],
		(sum(f.sale_grs)/(select sum(sale_grs) 
							from [dbo].[fct_cheque] as f
							inner join dim_goods as g
								on g.good_id = f.good_id
							inner join dim_date as d
								on d.did = f.date_id
							inner join [dim_stores] as s
								on s.store_id = f.store_id
							inner join dbo.dim_cash_register as cr
								on cr.cash_register_id = f.cash_register_id
							join string_split(@good_group_name, ',')
								on value = g.group_name
							where g.good_id = f.good_id and 
							date_id between @date_from_int and @date_to_int 							
									))*100 as [���� ������ � ���],
		(sum(f.cost_net)/nullif(sum(f.quantity),0)) as [������� ���� ������� ���., ��� ���],
		(sum(f.sale_net) - sum(f.cost_net))   as [����� ���. ��� ���],
		((sum(f.sale_net) - sum(f.cost_net))/(nullif(sum(f.cost_net),0)))*100 as [������� % ��� ���]		
	FROM [dbo].[fct_cheque] as f
	inner join dim_goods as g
		on g.good_id = f.good_id
	inner join [dim_stores] as s
		on s.store_id = f.store_id
	inner join dim_date as d
		on d.did = f.date_id
	inner join dbo.dim_cash_register as cr
		on cr.cash_register_id = f.cash_register_id	
	join STRING_SPLIT (@good_group_name, ',')
		on value = g.group_name
	where date_id between @date_from_int and @date_to_int	
	group by d.d,
		s.store_name,
		g.group_name,
		g.good_name